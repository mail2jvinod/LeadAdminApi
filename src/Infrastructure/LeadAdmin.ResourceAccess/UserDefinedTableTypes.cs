using System.Data;

namespace LeadAdmin.ResourceAccess
{
    public static class UserDefinedTableTypes
    {

        #region Institution
        public static DataTable Institution
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("Title", DbColumnType.String)
                    .AddColumn("Code", DbColumnType.String)

                    .AddColumn("MobileNumber", DbColumnType.String)
                    .AddColumn("Email", DbColumnType.String)
                    .AddColumn("Website", DbColumnType.String)

                    .AddColumn("CommAddress", DbColumnType.String)
                    .AddColumn("CityName", DbColumnType.String)
                    .AddColumn("StateName", DbColumnType.String)
                    .AddColumn("PINCode", DbColumnType.String)

                    .AddColumn("InStatus", DbColumnType.String);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region Domain
        public static DataTable NavigationItemType
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("NavigationUid", DbColumnType.UniqueIdentifier)

                    .AddColumn("Title", DbColumnType.String)
                    .AddColumn("NavigationPath", DbColumnType.String)
                    .AddColumn("DependentOn", DbColumnType.String)
                    .AddColumn("RedirectPath", DbColumnType.String)

                    .AddColumn("LevelId", DbColumnType.Byte)
                    .AddColumn("ParentUid", DbColumnType.UniqueIdentifier)
                    .AddColumn("ImagePath", DbColumnType.String)
                    .AddColumn("SortId", DbColumnType.Int)

                    .AddColumn("LocalStorageKey", DbColumnType.String)
                    .AddColumn("PageDataGridKey", DbColumnType.String)
                    .AddColumn("ModulesList", DbColumnType.String)
                    .AddColumn("IsFavorite", DbColumnType.Bool);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }

        public static DataTable LookupType
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)

                    .AddColumn("Title", DbColumnType.String)
                    .AddColumn("Category", DbColumnType.String)
                    .AddColumn("IsReadOnly", DbColumnType.Bool);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }

        public static DataTable LookupDetailType
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("LookupId", DbColumnType.Int)

                    .AddColumn("LookupName", DbColumnType.String)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("Title", DbColumnType.String)
                    .AddColumn("SequenceId", DbColumnType.Short);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region Academicyear
        public static DataTable AcademicYear
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("FromDate", DbColumnType.DateTime)
                    .AddColumn("ToDate", DbColumnType.DateTime)
                    .AddColumn("Title", DbColumnType.String);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region Location
        public static DataTable Location
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("Title", DbColumnType.String)
                    .AddColumn("Code", DbColumnType.String)
                    .AddColumn("Address", DbColumnType.String)
                    .AddColumn("City", DbColumnType.String)
                    .AddColumn("StateId", DbColumnType.Int)
                    .AddColumn("PINCode", DbColumnType.String);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region State
        public static DataTable State
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)

                    .AddColumn("Title", DbColumnType.String)
                    .AddColumn("Code", DbColumnType.String)
                    .AddColumn("GSTCode", DbColumnType.String);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region City
        public static DataTable City
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)

                    .AddColumn("Title", DbColumnType.String)
                    .AddColumn("Code", DbColumnType.String)
                    .AddColumn("GSTCode", DbColumnType.String);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region Country
        public static DataTable Country
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)

                    .AddColumn("Title", DbColumnType.String)
                    .AddColumn("Code", DbColumnType.String)
                    .AddColumn("GSTCode", DbColumnType.String);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region StudentType
        public static DataTable StudentType
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("StudentTypeId", DbColumnType.Int)

                    .AddColumn("Title", DbColumnType.String)
                    .AddColumn("Code", DbColumnType.String)

                    .AddColumn("GroupCode", DbColumnType.String);
                    
                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region FeeComponentType
        public static DataTable FeeComponentType
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("Title", DbColumnType.String)
                    .AddColumn("Code", DbColumnType.String)
                    .AddColumn("SequenceId", DbColumnType.Int);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region FeeComponent
        public static DataTable FeeComponent
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("Title", DbColumnType.String)
                    .AddColumn("Code", DbColumnType.String)
                    .AddColumn("HSNCode", DbColumnType.String)
                    .AddColumn("SequenceId", DbColumnType.Int);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region ConcessionType
        public static DataTable ConcessionType
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("Title", DbColumnType.String);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region StudentClass
        public static DataTable StudentClass
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("Title", DbColumnType.String)
                    .AddColumn("SortId", DbColumnType.Int);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region StudentSection
        public static DataTable StudentSection
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("Title", DbColumnType.String)
                    .AddColumn("SortId", DbColumnType.Int);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region FeeOrganization
        public static DataTable FeeOrganization
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("Title", DbColumnType.String)
                    .AddColumn("Code", DbColumnType.String)
                    .AddColumn("IsDefault", DbColumnType.Int);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region FeeSetup
        public static DataTable FeeSetup
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("AcademicYearId", DbColumnType.Int)
                    .AddColumn("LocationId", DbColumnType.Int)
                    .AddColumn("ClassId", DbColumnType.Int)
                    .AddColumn("FeeComponentId", DbColumnType.Int)
                    .AddColumn("FeeOrganizationId", DbColumnType.Int)

                    .AddColumn("GrossPupilFee", DbColumnType.Int)

                    .AddColumn("SGSTPer", DbColumnType.Decimal)
                    .AddColumn("CGSTPer", DbColumnType.Decimal)

                    .AddColumn("NetPupilFee", DbColumnType.Decimal)

                   .AddColumn("DueDate", DbColumnType.DateTime);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region FeeConcession
        public static DataTable FeeConcession
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("AcademicYearId", DbColumnType.Int)
                    .AddColumn("PupilId", DbColumnType.Int)
                    .AddColumn("ConcessionTypeId", DbColumnType.Int)
                    .AddColumn("ConcessionDate", DbColumnType.DateTime)
                    .AddColumn("ComponentId", DbColumnType.Int)

                    .AddColumn("ConcessionAmount", DbColumnType.Int)

                    .AddColumn("Remarks", DbColumnType.String);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region FeeReceipt
        public static DataTable FeeReceipt
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("AcademicYearId", DbColumnType.Int)
                    .AddColumn("PupilId", DbColumnType.Int)
                    .AddColumn("AcademicYearId", DbColumnType.Int)
                    .AddColumn("ReceiptDate", DbColumnType.DateTime)
                    .AddColumn("ModeOfPaymentId", DbColumnType.Int)

                    .AddColumn("ComponentTypeId", DbColumnType.Int)
                    .AddColumn("FeeOrganizationId", DbColumnType.Int)
                    .AddColumn("GSTAmount", DbColumnType.Decimal)
                    .AddColumn("Amount", DbColumnType.Int)
                    .AddColumn("Remarks", DbColumnType.String)
                    .AddColumn("FeeReceiptPgId", DbColumnType.Int)
                    .AddColumn("ComponentTypeId", DbColumnType.Int)

                    .AddColumn("ChequeNo", DbColumnType.String)
                    .AddColumn("ChequeDate", DbColumnType.DateTime)
                    .AddColumn("BankName", DbColumnType.String)
                    .AddColumn("IFSCNo", DbColumnType.String)
                    .AddColumn("UniqueId", DbColumnType.UniqueIdentifier)
                    .AddColumn("FeeReceiptSerialNumber", DbColumnType.Int)

                    .AddColumn("ManualReceiptNumber", DbColumnType.String)
                    .AddColumn("PrintReceiptNumber", DbColumnType.String)
                    .AddColumn("ReceiptStatus", DbColumnType.String);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region FeeRefund
        public static DataTable FeeRefund
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("AcademicYearId", DbColumnType.Int)
                    .AddColumn("PupilId", DbColumnType.Int)
                    .AddColumn("ReceiptDate", DbColumnType.DateTime)
                    .AddColumn("ModeOfPaymentId", DbColumnType.Int)

                    .AddColumn("ComponentTypeId", DbColumnType.Int)
                    .AddColumn("FeeOrganizationId", DbColumnType.Int)
                    .AddColumn("GSTAmount", DbColumnType.Decimal)
                    .AddColumn("Amount", DbColumnType.Int)
                    .AddColumn("Remarks", DbColumnType.String)

                    .AddColumn("ChequeNo", DbColumnType.String)
                    .AddColumn("BankName", DbColumnType.String)
                    .AddColumn("IFSCNo", DbColumnType.String)

                    .AddColumn("UniqueId", DbColumnType.UniqueIdentifier)
                    .AddColumn("FeeReceiptSerialNumber", DbColumnType.Int)
                    .AddColumn("ManualReceiptNumber", DbColumnType.String)
                    .AddColumn("PrintReceiptNumber", DbColumnType.String)

                    .AddColumn("ReceiptStatus", DbColumnType.String);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region Product
        public static DataTable Product
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("Title", DbColumnType.String)
                    .AddColumn("Code", DbColumnType.Int)
                    .AddColumn("HSNCode", DbColumnType.Int)

                    .AddColumn("DefaultGrossPrice", DbColumnType.Int)
                    .AddColumn("SGSTPer", DbColumnType.Decimal)
                    .AddColumn("CGSTPer", DbColumnType.Decimal);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region InventoryPriceSetup
        public static DataTable InventoryPriceSetup
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("AcademicYearId", DbColumnType.Int)
                    .AddColumn("LocationId", DbColumnType.Int)
                    .AddColumn("ClassId", DbColumnType.Int)

                    .AddColumn("ProductId", DbColumnType.Int)
                    .AddColumn("FeeOrganizationId", DbColumnType.Decimal)
                    .AddColumn("GrossPrice", DbColumnType.Decimal);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region FeeReceiptPg
        public static DataTable FeeReceiptPg
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("PupilId", DbColumnType.Int)
                    .AddColumn("AcademicYearId", DbColumnType.Int)
                    .AddColumn("ReceiptDate", DbColumnType.DateTime)
                    

                    .AddColumn("ComponentTypeId", DbColumnType.Int)
                    .AddColumn("FeeOrganizationId", DbColumnType.Int)
                    .AddColumn("GSTAmount", DbColumnType.Decimal)
                    .AddColumn("Amount", DbColumnType.Int)
                    .AddColumn("Remarks", DbColumnType.String)

                    .AddColumn("PgReceiptId", DbColumnType.Int)
                    .AddColumn("PgTransactionId", DbColumnType.Int)
                    .AddColumn("ComponentTypeId", DbColumnType.Int)
 
                    .AddColumn("UniqueId", DbColumnType.UniqueIdentifier)

                    .AddColumn("ReceiptStatus", DbColumnType.String);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region InventoryReceipt
        public static DataTable InventoryReceipt
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("AcademicYearId", DbColumnType.Int)
                    .AddColumn("PupilId", DbColumnType.Int)
                    .AddColumn("ReceiptDate", DbColumnType.DateTime)
                    .AddColumn("ModeOfPaymentId", DbColumnType.Int)

                    .AddColumn("ComponentTypeId", DbColumnType.Int)
                    .AddColumn("InventoryOrganizationId", DbColumnType.Int)
                    .AddColumn("GSTAmount", DbColumnType.Decimal)
                    .AddColumn("Amount", DbColumnType.Int)
                    .AddColumn("Remarks", DbColumnType.String)
                    .AddColumn("InventoryReceiptPgId", DbColumnType.Int)
                      

                    .AddColumn("ChequeNo", DbColumnType.String)
                    .AddColumn("ChequeDate", DbColumnType.DateTime)
                    .AddColumn("BankName", DbColumnType.String)
                    .AddColumn("IFSCNo", DbColumnType.String) 

                    .AddColumn("UniqueId", DbColumnType.UniqueIdentifier)
                    .AddColumn("ReceiptNumberPrefix", DbColumnType.UniqueIdentifier)
                    .AddColumn("InventoryReceiptSerialNumber", DbColumnType.Int)
                    .AddColumn("ManualReceiptNumber", DbColumnType.String)
                    .AddColumn("PrintReceiptNumber", DbColumnType.String)
                    .AddColumn("ReceiptStatus", DbColumnType.String);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region InventoryReceiptPg
        public static DataTable InventoryReceiptPg
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("PupilId", DbColumnType.Int)
                    .AddColumn("AcademicYearId", DbColumnType.Int)
                    .AddColumn("ReceiptDate", DbColumnType.DateTime)

                    .AddColumn("InventoryOrganizationId", DbColumnType.Int)
                    .AddColumn("GSTAmount", DbColumnType.Decimal)
                    .AddColumn("Amount", DbColumnType.Int)
                    .AddColumn("Remarks", DbColumnType.String)

                    .AddColumn("PgReceiptId", DbColumnType.Int)
                    .AddColumn("PgTransactionId", DbColumnType.Int)

                    .AddColumn("UniqueId", DbColumnType.UniqueIdentifier)

                    .AddColumn("ReceiptStatus", DbColumnType.String);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region InventoryRefund
        public static DataTable InventoryRefund
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("AcademicYearId", DbColumnType.Int)
                    .AddColumn("PupilId", DbColumnType.Int)
                    .AddColumn("ReceiptDate", DbColumnType.DateTime)
                    .AddColumn("ModeOfPaymentId", DbColumnType.Int)

                    .AddColumn("InventoryOrganizationId", DbColumnType.Int)
                    .AddColumn("GSTAmount", DbColumnType.Decimal)
                    .AddColumn("Amount", DbColumnType.Int)
                    .AddColumn("Remarks", DbColumnType.String)

                    .AddColumn("ChequeNo", DbColumnType.String)
                    .AddColumn("ChequeDate", DbColumnType.DateTime)
                    .AddColumn("BankName", DbColumnType.String)
                    .AddColumn("IFSCNo", DbColumnType.String)

                    .AddColumn("UniqueId", DbColumnType.UniqueIdentifier)
                    .AddColumn("ReceiptNumberPrefix", DbColumnType.UniqueIdentifier)
                    .AddColumn("InventoryReceiptSerialNumber", DbColumnType.Int)
                    .AddColumn("ManualReceiptNumber", DbColumnType.String)
                    .AddColumn("PrintReceiptNumber", DbColumnType.String)

                    .AddColumn("ReceiptStatus", DbColumnType.String);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

        #region Transportation
        public static DataTable VehicleService
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("AcademicYearId", DbColumnType.Int)
                    .AddColumn("LocationId", DbColumnType.Int)
                    .AddColumn("FeeOrganizationId", DbColumnType.String)

                    .AddColumn("ServiceName", DbColumnType.Int)
                    .AddColumn("RegNo", DbColumnType.Int);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }

        public static DataTable VehicleServiceRoute
        {
            get
            {
                var result = new DataTable();
                var columnCollection = new DataColumnCollection();

                columnCollection
                    .AddColumn("Id", DbColumnType.Int)
                    .AddColumn("InstitutionId", DbColumnType.Int)

                    .AddColumn("AcademicYearId", DbColumnType.Int)
                    .AddColumn("LocationId", DbColumnType.Int)
                    .AddColumn("FeeOrganizationId", DbColumnType.String)

                    .AddColumn("ServiceName", DbColumnType.Int)
                    .AddColumn("RegNo", DbColumnType.Int);

                foreach (var item in columnCollection) result.Columns.Add(item);

                return result;
            }
        }
        #endregion

    }
}

