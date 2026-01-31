using Newtonsoft.Json.Linq;

namespace LeadAdmin.Utilities.Helpers
{
    public class DataSetDataQr 
    {
        private int dataSetId;
        private long id;
        private string uniqueId;
        private string jsonData;

        public DataSetDataQr(int dataSetId, long id, string uniqueId, string jsonData)
        {
            this.dataSetId = dataSetId;
            this.id = id;
            this.uniqueId = uniqueId;
            this.jsonData = jsonData;
        }        

        
        public override string ToString()
        {
            dynamic dataSetheader = new JObject();            

            dataSetheader.DataSetId = dataSetId;
            dataSetheader.Id = id;
            dataSetheader.UniqueId = uniqueId;
            //dataSetheader.JsonData = jsonData;            

            return Newtonsoft.Json.JsonConvert.SerializeObject(dataSetheader);
        }
    }
}
