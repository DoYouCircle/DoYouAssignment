using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouAssignment.database
{
	class Element
	{
		public static Database db = new Database("default_database");
	}
	
	class Course : Element
	{
		public bool inDatabase = false;

		private int? id = null;
		public int? Id
		{
			get { return this.id; }
			set { this.id = value; }
		}
		private string name;
		public string Name
		{
			get { return this.name; }
			set { this.name = value; if (inDatabase) { db.UpdateCourse(id: (int)this.id, name: value); } }
		}
		private string description;
		public string Description
		{
			get { return this.description; }
			set { this.description = value; if (inDatabase) { db.UpdateCourse(id: (int)this.id, description: value); } }
		}
		private string semester;
		public string Semester
		{
			get { return this.semester; }
			set { this.semester = value; if (inDatabase) { db.UpdateCourse(id: (int)this.id, semester: value); } }
		}
		private string subject;
		public string Subject
		{
			get { return this.subject; }
			set { this.subject = value; if (inDatabase) { db.UpdateCourse(id: (int)this.id, subject: value); } }
		}

		public void ToDatabase()
		{
			Id = db.InsertCourse(name : Name, description: Description, semester: Semester, subject: Subject);
			inDatabase = true;
		}
	}

	class AssignmentGroup : Element
	{
		public bool inDatabase = false;

		private int? id = null;
		public int? Id
		{
			get { return this.id; }
			set { this.id = value; }
		}
		private int courseId;
		public int CourseId
		{
			get { return this.courseId; }
			set { this.courseId = value; if (inDatabase) { db.UpdateAssignmentGroup(id: (int)this.id, course_id: value); } }
		}
		private string name;
		public string Name
		{
			get { return this.name; }
			set { this.name = value; if (inDatabase) { db.UpdateAssignmentGroup(id: (int)this.id, name: value); } }
		}
		private string category;
		public string Category
		{
			get { return this.category; }
			set { this.category = value; if (inDatabase) { db.UpdateAssignmentGroup(id: (int)this.id, category: value); } }
		}
		private string description;
		public string Description
		{
			get { return this.description; }
			set { this.description = value; if (inDatabase) { db.UpdateAssignmentGroup(id: (int)this.id, description: value); } }
		}

		public void ToDatabase()
        {
			Id = db.InsertAssignmentGroup(CourseId, Name, Category, Description);
			inDatabase = true;
		}
	}

	class Assignment : Element
	{
		public bool inDatabase = false;

		private int? id = null;
		public int? Id
		{
			get { return this.id; }
			set { this.id = value; }
		}
		private int assignmentGroupId;
		public int AssignmentGroupId
		{
			get { return this.assignmentGroupId; }
			set { this.assignmentGroupId = value; if (inDatabase) { db.UpdateAssignment(id: (int)this.id, assignment_group_id: value); } }
		}
		private string name;
		public string Name
		{
			get { return this.name; }
			set { this.name = value; if (inDatabase) { db.UpdateAssignment(id: (int)this.id, name: value); } }
		}
		private string dueDate;
		public string DueDate
		{
			get { return this.dueDate; }
			set { this.dueDate = value; if (inDatabase) { db.UpdateAssignment(id: (int)this.id, due_date: value); } }
		}
		private int? score;
		public int? Score
		{
			get { return this.score; }
			set { this.score = value; if (inDatabase) { db.UpdateAssignment(id: (int)this.id, score: value); } }
		}
		private int? maxPoints;
		public int? MaxPoints
		{
			get { return this.maxPoints; }
			set { this.maxPoints = value; if (inDatabase) { db.UpdateAssignment(id: (int)this.id, max_points: value); } }
		}
		private string comment;
		public string Comment
		{
			get { return this.comment; }
			set { this.comment = value; if (inDatabase) { db.UpdateAssignment(id: (int)this.id, comment: value); } }
		}
		private bool? submitted;
		public bool? Submitted
		{
			get { return this.submitted; }
			set { this.submitted = value; if (inDatabase) { db.UpdateAssignment(id: (int)this.id, submitted: value); } }
		}

		public void ToDatabase()
		{
			Id = db.InsertAssignment(AssignmentGroupId, Name, DueDate, Score, MaxPoints, Comment, Submitted);
			inDatabase = true;
		}
	}
}
