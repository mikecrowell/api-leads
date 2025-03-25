using System.ComponentModel.DataAnnotations;

namespace api.leads.DTO.Request
{
	public class PersonPostRequest
	{
		[Required]
		public int? callId { get; set; }

		[Required]
		[MaxLength(30)]
		public string personType { get; set; }

		[Required]
		public bool? reflectsUpdates { get; set; }

		[MaxLength(88)]
		public string fullName { get; set; }

		[MaxLength(30)]
		public string firstName { get; set; }

		[MaxLength(25)]
		public string paternalLastName { get; set; }

		[MaxLength(25)]
		public string maternalLastName { get; set; }

		[Required]
		[MaxLength(64)]
		public string streetAddress { get; set; }

		[Required]
		[MaxLength(25)]
		public string city { get; set; }

		[Required]
		[MaxLength(2)]
		public string state { get; set; }

		[Required]
		[MaxLength(3)]
		public string country { get; set; }

		[Required]
		[MaxLength(10)]
		public string zipCode { get; set; }

		[MaxLength(12)]
		public string homePhone { get; set; }

		[MaxLength(35)]
		public string cellPhone { get; set; }

		[MaxLength(12)]
		public string workPhone { get; set; }

		public int? customerNumber { get; set; }

		[MaxLength(2)]
		public string customerClient { get; set; }

		public int? distributorNumber { get; set; }

		[MaxLength(12)]
		public string distributorClient { get; set; }

		[MaxLength(12)]
		public string serviceRep { get; set; }

		[MaxLength(2)]
		public string language { get; set; }

		public bool isActive { get; set; }

		public bool isInHouseAccount { get; set; }

		[MaxLength(25)]
		public string userId { get; set; }
	}
}
