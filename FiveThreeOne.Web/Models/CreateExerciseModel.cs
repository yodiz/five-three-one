using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FiveThreeOne.Web.Models {

	public class CreateExerciseModel {
		[Required]
		[Display(Name = "Övningens namn")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Träningsmax")]
		public double WorkoutMax { get; set; }
	}
}
