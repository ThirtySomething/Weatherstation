using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace net.derpaul.tf.plugin
{
    interface IDMInterface
    {
        Task<IDMInterface> FindByID(long ID);

        Task<IDMInterface> FindByName(MeasurementValue SensorValue);

        Task Init();

        Task<IDMInterface> Insert(MeasurementValue SensorValue);
    }
}
