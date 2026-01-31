using LeadAdmin.BusinessAccess.Contracts;
using LeadAdmin.Entities.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeadAdmin.API.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [DigiCollectExceptionFilter]
    public class CommonApiController : DigiCollectController
    {
        public CommonApiController(UserContext userContext)
        {
            this.userContext = userContext;
        }

        [Route("[action]")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> FileUpload(IFormFile file, string Title)
        {
            if (file == null) return Ok(false);
            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    string filename = Title + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                    path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Uploads"));
                    using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                    }
                }
                return Ok(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UploadSingleFile(IFormFile file)
        {
            if (file == null) return Ok(false);
            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Uploads"));
                    using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                    }
                }
                return Ok(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files.Count == 0) return Ok(false);
            string path = "";
            try
            {
                for (int a = 0; a < files.Count; a++)
                {
                    string filename = Guid.NewGuid() + "_" + Path.GetExtension(files[a].FileName);
                    path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Uploads"));
                    using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    {
                        await files[a].CopyToAsync(filestream);
                    }
                }
                return Ok(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[Route("[action]")]
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> InsertInward(List<IFormFile> files, string registrationNo)
        //{
        //    if (files.Count == 0 || registrationNo.Length == 0) return Ok(false);
        //    string path = "";
        //    try
        //    {
        //        for (int a = 0; a < files.Count; a++)
        //        {
        //            string filename = "I_" + Guid.NewGuid() + "_" + registrationNo + "_" + DateTime.Now.ToString("ddMMyyHHMM") + "_" + Path.GetExtension(files[a].FileName);
        //            path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Uploads"));
        //            using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
        //            {
        //                await files[a].CopyToAsync(filestream);
        //            }
        //        }
        //        return Ok(true);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> InsertOutward(List<IFormFile> files, int InwardId)
        //{
        //    if (files.Count == 0 || InwardId == 0) return Ok(false);
        //    string registrationNo = "", path = "";
        //    try
        //    {
        //        for (int a = 0; a < files.Count; a++)
        //        {
        //            string filename = "O_" + Guid.NewGuid() + "_" + registrationNo + "_" + DateTime.Now.ToString("ddMMyyHHMM") + "_" + Path.GetExtension(files[a].FileName);
        //            path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Uploads"));
        //            using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
        //            {
        //                await files[a].CopyToAsync(filestream);
        //            }
        //        }
        //        return Ok(true);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[Route("[action]")]
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetProducts(int productId, int categoryId)
        //{
        //    try
        //    {
        //        List<Product> products = new List<Product>();
        //        products.Add(new Product { ProductId = 1, ProductCode = "A", ProductName = "Apple", CategoryId = 1, ManufacturerId = 1 });
        //        products.Add(new Product { ProductId = 2, ProductCode = "B", ProductName = "Ball", CategoryId = 1, ManufacturerId = 1 });
        //        products.Add(new Product { ProductId = 3, ProductCode = "C", ProductName = "Cat", CategoryId = 1, ManufacturerId = 1 });
        //        products.Add(new Product { ProductId = 4, ProductCode = "D", ProductName = "Doll", CategoryId = 1, ManufacturerId = 1 });
        //        products.Add(new Product { ProductId = 5, ProductCode = "E", ProductName = "Elephant", CategoryId = 1, ManufacturerId = 1 });
        //        products.Add(new Product { ProductId = 6, ProductCode = "F", ProductName = "Fan", CategoryId = 1, ManufacturerId = 1 });
        //        products.Add(new Product { ProductId = 7, ProductCode = "G", ProductName = "Gun", CategoryId = 1, ManufacturerId = 1 });
        //        products.Add(new Product { ProductId = 8, ProductCode = "H", ProductName = "Hippo", CategoryId = 1, ManufacturerId = 1 });
        //        products.Add(new Product { ProductId = 9, ProductCode = "I", ProductName = "India", CategoryId = 1, ManufacturerId = 1 });
        //        return Ok(products);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



    }

}
