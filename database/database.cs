using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace DoYouAssignment.database
{
	class Database

	{
		private readonly string FK = "PRAGMA foreign_keys = ON; ";
		public SQLiteConnection connection;

		public enum TABLES
		{
			courses,
			assignmentGroups,
			assignments
		}

		private string N(object param)
		{
			if (param == null)
				return "NULL";
			else
				return "'" + param + "'";
		}

		private List<Dictionary<string, object>> ToDict(SQLiteDataReader reader)
		{
			List<Dictionary<string, object>> query = new List<Dictionary<string, object>>();
			while (reader.Read())
			{
				query.Add(Enumerable.Range(0, reader.FieldCount).ToDictionary(i => reader.GetName(i), i => reader.GetValue(i)));
			}
			return query;
		}

		private object CastNulls(object input)
        {
			if (input is System.DBNull)
            {
				return null;
            }
			else
            {
				return input;
            }
        }

		private List<object> ToClass(SQLiteDataReader reader, TABLES table)
		{
			List<object> query = new List<object>();

			while (reader.Read())
			{
				switch (table)
				{
					case TABLES.courses:
						query.Add(new database.Course() { 
							Id = (int)(long)CastNulls(reader.GetValue(0)), 
							Name = (string)CastNulls(reader.GetValue(1)),
							Description = (string)CastNulls(reader.GetValue(2)),
							Semester = (string)CastNulls(reader.GetValue(3)),
							Subject = (string)CastNulls(reader.GetValue(4)),
							inDatabase = true,
						}); break;
					case TABLES.assignmentGroups:
						query.Add(new database.AssignmentGroup()
						{
							Id = (int)(long)CastNulls(reader.GetValue(0)),
							CourseId = (int)CastNulls(reader.GetValue(1)),
							Name = (string)CastNulls(reader.GetValue(2)),
							Category = (string)CastNulls(reader.GetValue(3)),
							Description = (string)CastNulls(reader.GetValue(4)),
							inDatabase = true,
						}); break;
					case TABLES.assignments:
						int a = (int)(long)CastNulls(reader.GetValue(0));
						int b = (int)CastNulls(reader.GetValue(1));
						string c = (string)CastNulls(reader.GetValue(2));
						query.Add(new database.Assignment()
						{
							Id = (int)(long)CastNulls(reader.GetValue(0)),
							AssignmentGroupId = (int)CastNulls(reader.GetValue(1)),
							Name = (string)CastNulls(reader.GetValue(2)),
							DueDate = null,
							Score = (int?)CastNulls(reader.GetValue(4)),
							MaxPoints = (int?)CastNulls(reader.GetValue(5)),
							Comment = (string)CastNulls(reader.GetValue(6)),
							Submitted = (bool?)CastNulls(reader.GetValue(7)),
							inDatabase = true,
						}); break;
					default:
						continue;
				}
			}
			return query;
		}

		public Database(string name)
		{
			OpenDataBase(name);
			InitializeDatabase();
		}

		public void OpenDataBase(string name)
		{
			connection = new SQLiteConnection(String.Format("Data Source={0}.db", name));
		}

		public void InitializeDatabase()
		{
			connection.Open();
			SQLiteCommand cmd = connection.CreateCommand();
			cmd.CommandText = File.ReadAllText("./database/database_structure.sql");
			cmd.ExecuteNonQuery();
			connection.Close();
		}

		public int InsertCourse(
			string name = null,
			string description = null,
			string semester = null,
			string subject = null
			)
		{
			connection.Open();
			SQLiteCommand cmd = connection.CreateCommand();
			cmd.CommandText = String.Format(FK + "INSERT INTO courses"
				+ "(name, description, semester, subject)"
				+ "VALUES ({0}, {1}, {2}, {3})",
				N(name), N(description), N(semester), N(subject));
			cmd.ExecuteNonQuery();
			int id = (int)connection.LastInsertRowId;
			connection.Close();
			return id;
		}

		public int InsertAssignmentGroup(
			int course_id,
			string name = null,
			string category = null,
			string description = null
			)
		{
			connection.Open();
			SQLiteCommand cmd = connection.CreateCommand();
			cmd.CommandText = String.Format(FK + "INSERT INTO assignment_groups "
				+ "(course_id, name, category, description)"
				+ "VALUES ({0}, {1}, {2}, {3})",
				course_id, N(name), N(category), N(description));
			cmd.ExecuteNonQuery();
			int id = (int)connection.LastInsertRowId;
			connection.Close();
			return id;
		}

		public int InsertAssignment(
			int assignment_group_id,
			string name = null,
			string due_date = null,
			int? score = null,
			int? max_points = null,
			string comment = null,
			bool? submitted = null
			)
		{
			connection.Open();
			SQLiteCommand cmd = connection.CreateCommand();
			cmd.CommandText = String.Format(FK + "INSERT INTO assignments "
				+ "(assignment_group_id, name, due_date, score, max_points, comment, submitted) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6})",
				assignment_group_id, N(name), N(due_date), N(score), N(max_points), N(comment), N(submitted));
			cmd.ExecuteNonQuery();
			int id = (int)connection.LastInsertRowId;
			connection.Close();
			return id;
		}

		public void DeleteCourse(int id)
		{
			connection.Open();
			SQLiteCommand cmd = connection.CreateCommand();
			cmd.CommandText = String.Format(FK + "DELETE FROM courses WHERE id={0}", id);
			cmd.ExecuteNonQuery();
			connection.Close();
		}
		public void DeleteAssignmentGroup(int id)
		{
			connection.Open();
			SQLiteCommand cmd = connection.CreateCommand();
			cmd.CommandText = String.Format(FK + "DELETE FROM assignment_groups WHERE id={0}", id);
			cmd.ExecuteNonQuery();
			connection.Close();
		}
		public void DeleteAssignment(int id)
		{
			connection.Open();
			SQLiteCommand cmd = connection.CreateCommand();
			cmd.CommandText = String.Format(FK + "DELETE FROM assignments WHERE id={0}", id);
			cmd.ExecuteNonQuery();
			connection.Close();
		}


		public void UpdateCourse(
			int id,
			string name = null,
			string description = null,
			string semester = null,
			string subject = null
			)
		{
			connection.Open();
			SQLiteCommand cmd = connection.CreateCommand();
			cmd.CommandText = String.Format(FK + "UPDATE courses SET "
				+ "name = COALESCE({0}, name), description = COALESCE({1}, description), semester = COALESCE({2}, semester), subject = COALESCE({3}, subject) "
				+ "WHERE id = {4}",
				N(name), N(description), N(semester), N(subject), id);
			cmd.ExecuteNonQuery();
			connection.Close();
		}

		public void UpdateAssignmentGroup(
			int id,
			int? course_id = null,
			string name = null,
			string category = null,
			string description = null
			)
		{
			connection.Open();
			SQLiteCommand cmd = connection.CreateCommand();
			cmd.CommandText = String.Format(FK + "UPDATE assignment_groups SET "
				+ "course_id = COALESCE({0}, course_id), name = COALESCE({1}, name), category = COALESCE({2}, category), description = COALESCE({3}, description) "
				+ "WHERE id = {4}",
				N(course_id), N(name), N(category), N(description), id);
			cmd.ExecuteNonQuery();
			connection.Close();
		}

		public void UpdateAssignment(
			int id,
			int? assignment_group_id = null,
			string name = null,
			string due_date = null,
			int? score = null,
			int? max_points = null,
			string comment = null,
			bool? submitted = null
			)
		{
			connection.Open();
			SQLiteCommand cmd = connection.CreateCommand();
			cmd.CommandText = String.Format(FK + "UPDATE assignments SET "
				+ "assignment_group_id = COALESCE({0}, assignment_group_id), name = COALESCE({1}, name), due_date = COALESCE({2}, due_date), "
				+ "score = COALESCE({3}, score), max_points = COALESCE({4}, max_points), comment = COALESCE({5}, comment), submitted = COALESCE({6}, submitted) "
				+ "WHERE id = {7}",
				N(assignment_group_id), N(name), N(due_date), N(score), N(max_points), N(comment), N(submitted), id);
			cmd.ExecuteNonQuery();
			connection.Close();
		}

		public List<object> Select(TABLES table, string filter = null)
		{
			string table_string;
			switch (table)
			{
				case TABLES.courses:
					table_string = "courses"; break;
				case TABLES.assignmentGroups:
					table_string = "assignment_groups"; break;
				case TABLES.assignments:
					table_string = "assignments"; break;
				default:
					table_string = ""; break;
			}

			connection.Open();
			SQLiteCommand cmd = connection.CreateCommand();
			if (filter == null)
				cmd.CommandText = String.Format(FK + "SELECT * FROM {0}", table_string);
			else
				cmd.CommandText = String.Format(FK + "SELECT * FROM {0} WHERE {1}", table_string, filter);
			SQLiteDataReader reader = cmd.ExecuteReader();
			List<object> query = ToClass(reader, table);
			connection.Close();
			return query;
		}
	}

	class TestDatabase
	{
		public static void Test()
		{
			Database db = Element.db;

			db.InsertCourse("Experimentalphysik");
			db.InsertAssignmentGroup(1, "Übungsblätter");
			db.InsertAssignment(1, "Blatt 1");

			db.UpdateCourse(1, name: "newcoursename", description: "description");
			db.UpdateAssignmentGroup(1, name: "newgroupname");
			db.UpdateAssignment(1, name: "newassignment");

			db.DeleteCourse(2);
			db.DeleteAssignmentGroup(2);
			db.DeleteAssignment(2); 

			List<object> query_a = db.Select(Database.TABLES.courses);
			List<object> query_b = db.Select(Database.TABLES.assignmentGroups);
			List<object> query_c = db.Select(Database.TABLES.assignments);


			Course a = new Course() { Name = "new course" };
			a.ToDatabase();

		}
	}
}
