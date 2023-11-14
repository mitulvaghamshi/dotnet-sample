using System.ComponentModel.DataAnnotations;

namespace MovieTracker.Models
{
	public class DateSeenValidation : ValidationAttribute
	{
		public override bool IsValid(object? value) => Convert.ToDateTime(value) <= DateTime.Now;
	}
}
