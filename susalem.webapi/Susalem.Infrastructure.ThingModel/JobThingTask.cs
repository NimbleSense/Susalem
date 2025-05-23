using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Susalem.Infrastructure.ThingModel
{
    [PersistJobDataAfterExecution, DisallowConcurrentExecution]
    public class JobThingTask: IJob
    {
        public string Name { get; set; }
        public string Age { get; set; }

        public string IsRun { get; set; }


        public async Task Execute(IJobExecutionContext context)
        {
            // Todo 解析ThingModel 中的Command
        }
    }
}
