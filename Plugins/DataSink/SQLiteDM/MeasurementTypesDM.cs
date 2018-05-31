using programmersdigest.DataMapper.Attributes;
using programmersdigest.DataMapper.Migration;
using programmersdigest.DataMapper;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace net.derpaul.tf.plugin
{
    [Name("MeasurementTypes")]
    public class MeasurementTypesDM : IDMInterface
    {
        protected Database DBConnection { get; set; }

        [PrimaryKey]
        public long mt_id { get; set; }

        public string mt_name { get; set; }

        public MeasurementTypesDM(Database dbConnection)
        {
            DBConnection = dbConnection;
        }

        public async Task<IDMInterface> FindByID(long ID)
        {
            return await DBConnection.SelectSingle<MeasurementTypesDM>("SELECT * FROM \"measurementtypes\" WHERE mt_id = @id", new Dictionary<string, object>
            {
                ["@id"] = ID
            });
        }

        public async Task<IDMInterface> FindByName(MeasurementValue SensorValue)
        {
            return await DBConnection.SelectSingle<MeasurementTypesDM>("SELECT * FROM \"measurementtypes\" WHERE mt_name = @name", new Dictionary<string, object>
            {
                ["@name"] = SensorValue.Name
            });
        }

        public async Task Init()
        {
            await DBConnection.Execute(@"CREATE TABLE IF NOT EXISTS measurementtypes (
                mt_id INTEGER PRIMARY KEY,
                mt_name TEXT NOT NULL
            );");
        }

        public async Task<IDMInterface> Insert(MeasurementValue SensorValue)
        {
            mt_name = SensorValue.Name;
            var id = await DBConnection.Insert(this);
            return await FindByID(id);
        }
    }
}
