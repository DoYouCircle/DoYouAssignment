using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace DoYouAssignment.database
{
	class Database

	{
		private string NULL = DBNull.Value.ToString();
		private string FK = "PRAGMA foreign_keys = ON; ";
		public SQLiteConnection connection;
		SQLiteCommand cmd;

		private string n(object param)
		{
			if (param == null)
				return "NULL";
			else
				return "'" + param + "'";
		}

		private List<Dictionary<string, object>> toDict(SQLiteDataReader reader)
		{
			List<Dictionary<string, object>> query = new List<Dictionary<string, object>>();
			while (reader.Read())
			{
				query.Add(Enumerable.Range(0, reader.FieldCount).ToDictionary(i => reader.GetName(i), i => reader.GetValue(i)));
			}
			return query;
		}

		public void openDataBase(string name)
		{
			connection = new SQLiteConnection(String.Format("Data Source={0}.db", name));
			cmd = connection.CreateCommand();
		}

		public void initializeDatabase()
		{
			connection.Open();
			cmd.CommandText = File.ReadAllText("E:/Source/doyou/test/test/database.sql");
			cmd.ExecuteNonQuery();
			connection.Close();
		}

		public void insertCourse(
			string name = null,
			string description = null,
			string semester = null,
			string subject = null
			)
		{
			connection.Open();
			cmd.CommandText = String.Format(FK + "INSERT INTO courses"
				+ "(name, description, semester, subject)"
				+ "VALUES ({0}, {1}, {2}, {3})",
				n(name), n(description), n(semester), n(subject));
			cmd.ExecuteNonQuery();
			connection.Close();
		}

		public void insertAssignmentGroup(
			int course_id,
			string name = null,
			string category = null,
			string description = null
			)
		{
			connection.Open();
			cmd.CommandText = String.Format(FK + "INSERT INTO assignment_groups "
				+ "(course_id, name, category, description)"
				+ "VALUES ({0}, {1}, {2}, {3})",
				course_id, n(name), n(category), n(description));
			cmd.ExecuteNonQuery();
			connection.Close();
		}

		public void insertAssignment(
			int assignment_group_id,
			string name = null,
			string due_date = null,
			int? score = null,
			int? max_points = null,
			string comment = null,
			string submitted = null
			)
		{
			connection.Open();
			cmd.CommandText = String.Format(FK + "INSERT INTO assignments "
				+ "(assignment_group_id, name, due_date, score, max_points, comment, submitted) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6})",
				assignment_group_id, n(name), n(due_date), n(score), n(max_points), n(comment), n(submitted));
			cmd.ExecuteNonQuery();
			connection.Close();
		}

		public void deleteCourse(int id)
		{
			connection.Open();
			cmd.CommandText = String.Format(FK + "DELETE FROM courses WHERE id={0}", id);
			cmd.ExecuteNonQuery();
			connection.Close();
		}
		public void deleteAssignmentGroup(int id)
		{
			connection.Open();
			cmd.CommandText = String.Format(FK + "DELETE FROM assignment_groups WHERE id={0}", id);
			cmd.ExecuteNonQuery();
			connection.Close();
		}
		public void deleteAssignment(int id)
		{
			connection.Open();
			cmd.CommandText = String.Format(FK + "DELETE FROM assignments WHERE id={0}", id);
			cmd.ExecuteNonQuery();
			connection.Close();
		}


		public void updateCourse(
			int id,
			string name = null,
			string description = null,
			string semester = null,
			string subject = null
			)
		{
			connection.Open();
			cmd.CommandText = String.Format(FK + "UPDATE courses SET "
				+ "name = COALESCE({0}, name), description = COALESCE({1}, description), semester = COALESCE({2}, semester), subject = COALESCE({3}, subject) "
				+ "WHERE id = {4}",
				n(name), n(description), n(semester), n(subject), id);
			cmd.ExecuteNonQuery();
			connection.Close();
		}

		public void updateAssignmentGroup(
			int id,
			int? course_id = null,
			string name = null,
			string category = null,
			string description = null
			)
		{
			connection.Open();
			cmd.CommandText = String.Format(FK + "UPDATE assignment_groups SET "
				+ "course_id = COALESCE({0}, course_id), name = COALESCE({1}, name), category = COALESCE({2}, category), description = COALESCE({3}, description) "
				+ "WHERE id = {4}",
				n(course_id), n(name), n(category), n(description), id);
			cmd.ExecuteNonQuery();
			connection.Close();
		}

		public void updateAssignment(
			int id,
			int? assignment_group_id = null,
			string name = null,
			string due_date = null,
			int? score = null,
			int? max_points = null,
			string comment = null,
			string submitted = null
			)
		{
			connection.Open();
			cmd.CommandText = String.Format(FK + "UPDATE assignments SET "
				+ "assignment_group_id = COALESCE({0}, assignment_group_id), name = COALESCE({1}, name), due_date = COALESCE({2}, due_date), "
				+ "score = COALESCE({3}, score), max_points = COALESCE({4}, max_points), comment = COALESCE({5}, comment), submitted = COALESCE({6}, submitted) "
				+ "WHERE id = {7}",
				n(assignment_group_id), n(name), n(due_date), n(score), n(max_points), n(comment), n(submitted), id);
			cmd.ExecuteNonQuery();
			connection.Close();
		}

		public List<Dictionary<string, object>> select(string table, string filter = null)
		{
			connection.Open();
			if (filter == null)
				cmd.CommandText = String.Format(FK + "SELECT * FROM {0}", table);
			else
				cmd.CommandText = String.Format(FK + "SELECT * FROM {0} WHERE {1}", table, filter);
			SQLiteDataReader reader = cmd.ExecuteReader();
			List<Dictionary<string, object>> query = toDict(reader);
			connection.Close();
			return query;
		}
	}

	class testDatabase
	{
		public static void test()
		{
			database.Database db = new database.Database();
			db.openDataBase("db");
			db.initializeDatabase();

			db.insertCourse("Experimentalphysik");
			db.insertAssignmentGroup(1, "Übungsblätter");
			db.insertAssignment(1, "Blatt 1");

			db.updateCourse(1, name: "newcoursename");
			db.updateAssignmentGroup(1, name: "newgroupname");
			db.updateAssignment(1, name: "newassignment");

			db.deleteCourse(2);
			db.deleteAssignmentGroup(2);
			db.deleteAssignment(2);

			List<Dictionary<string, object>> query = db.select("courses", "id = 1");

			Console.WriteLine("");
		}
	}
}
