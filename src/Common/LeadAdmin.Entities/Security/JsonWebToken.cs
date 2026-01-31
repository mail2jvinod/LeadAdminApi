namespace LeadAdmin.Entities.Security
{
    public class JsonWebToken
    {
        public JsonWebToken()
        {
            this.LoginEntities = new List<vLoginEntity>();
        }
        public string access_token { get; set; }
        public string token_type { get; set; } = "bearer";
        public int expires_in { get; set; }
        public string refresh_token { get; set; }

        public string LoginScope { get; set; }
        public int UserId { get; set; }
        public int StudentId { get; set; }

        public int InstitutionId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }

        public bool IsSuccess { get; set; } = true;
        public bool IsLocked { get; set; } = false;
        public bool IsShowUsers { get; set; } = false;
        public string ErrorMessage { get; set; } = "";
        public DateTime expires_on { get; set; }

        public List<vLoginEntity> LoginEntities { get; set; }
    }
}
