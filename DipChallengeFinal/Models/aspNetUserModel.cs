using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DipChallengeFinal.Models
{
	public class aspNetUserModel
	{
		public List<AspNetUser> users { get; set; }

		public List<Game> Games { get; set; }

		public AspNetUser AspNetUser { get; set; }
	}
}