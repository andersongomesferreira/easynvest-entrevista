using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easynvest.Api.Options
{
	public class PoliciesOptions
	{
		public WaitAndRetryConfigOptions WaitAndRetryConfig { get; set; }		
	}
	public class WaitAndRetryConfigOptions
	{
		public int Retry { get; set; }
		public int Wait { get; set; }
		public int Timeout { get; set; }
	}
}
