using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Policy;

namespace LeadAdmin.Utilities.Helpers
{
    public static class PrintCodeHelper
    {
        public static MemoryStream GetQrCode(string url)
        {
            var ms = new MemoryStream();
            //var generator = new PayloadGenerator.Url(url);
            //string payload = generator.ToString();

            //using (var qrGenerator = new QRCodeGenerator())
            //{
            //    var qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            //    var qrCode = new QRCode(qrCodeData);
            //    var image = qrCode.GetGraphic(20);

            //    image.Save(ms, ImageFormat.Jpeg);
            //}

            return ms;
        }

        public static MemoryStream GetQrCodeData(int dataSetId, long id, string uniqueId, string jsonData)
        {
            var ms = new MemoryStream();
            //var generator = new DataSetDataQr(dataSetId, id, uniqueId, jsonData);
            //string payload = generator.ToString();

            //using (var qrGenerator = new QRCodeGenerator())
            //{
            //    var qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            //    var qrCode = new QRCode(qrCodeData);
            //    var image = qrCode.GetGraphic(20);

            //    image.Save(ms, ImageFormat.Jpeg);
            //}

            return ms;
        }

        public static MemoryStream GetBarCode(int dataSetId, long id)
        {
            var value = String.Format("{0:00000}", dataSetId) + String.Format("{0:0000000000}", id);
            var ms = new MemoryStream();
            //TODO: fix this
            //Barcode barcode = new Barcode();

            //using (var barcodeImage = barcode.Encode(BarcodeLib.Core.TYPE.CODE39, value, Color.Black, Color.White, 290, 120))
            //{
            //    barcodeImage.Save(ms, ImageFormat.Jpeg);
            //}

            return ms;
        }
    }
}
