//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using FiveThreeOne.Service;
//using FiveThreeOne.Web.Models;

//namespace FiveThreeOne.Web.Controllers {

//	[Authorize]
//	public class ExerciseController : Controller {
//		private readonly IExercise _exercise;
//		private readonly IIdentification _identification;

//		public ExerciseController(IIdentification identification, IExercise exercise) {
//			_exercise = exercise;
//			_identification = identification;
//		}

//		public ActionResult Create() {
//			return View();
//		}

//		[HttpPost]
//		public ActionResult Create(CreateExerciseModel createModel) {
//			if (this.ModelState.IsValid) {
//				var identity = _identification.Authenticate(this.User.Identity.Name);
//				_exercise.CreateExercise(identity, new Model.NamedExercise(createModel.Name, new Model.Exercise(createModel.WorkoutMax, Model.ExerciseType.Main)));

//				return Display(createModel.Name);
//			}
//			else {
//				ModelState.AddModelError("", "Error in some parameter");
//				return View(createModel);
//			}
//		}

//		public ActionResult Display(string id) {
//			var identity = _identification.Authenticate(this.User.Identity.Name);
//			var exercise = _exercise.GetExercise(identity, id).Value;

//			var week1Sets = _exercise.CalculateExerciseSets(exercise.Exercise, Model.Week.Week1);
//			var week2Sets = _exercise.CalculateExerciseSets(exercise.Exercise, Model.Week.Week2);
//			var week3Sets = _exercise.CalculateExerciseSets(exercise.Exercise, Model.Week.Week3);
//			var week4Sets = _exercise.CalculateExerciseSets(exercise.Exercise, Model.Week.Week4);

//			var model = new ExerciseDisplayModel(exercise, week1Sets, week2Sets, week3Sets, week4Sets);

//			return View("Display", model);
//		}



//	}
//}
