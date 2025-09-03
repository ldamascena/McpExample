using MCPServer.Clients;
using MCPServer.DTOs;
using ModelContextProtocol.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPServer.Tools
{
    [McpServerToolType]
    public static class StudentsTools
    {
        [McpServerTool, Description("Get all students")]
        public static async Task<string> GetStudents(StudentsApiClient studentsApiClient)
        {
            try
            {
                var students = await studentsApiClient.GetStudents();
                if (students == null || !students.Any())
                    return "No students found.";
                var result = new StringBuilder();
                foreach (var student in students)
                {
                    result.AppendLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}");
                }
                return result.ToString();
            }
            catch (Exception ex)
            {
                return $"Error retrieving students. {ex.Message}";
            }
        }

        [McpServerTool, Description("Get a student by ID")]
        public static async Task<string> GetStudent(StudentsApiClient studentsApiClient, Guid id)
        {
            try
            {
                var student = await studentsApiClient.GetStudent(id);
                if (student == null)
                    return "Student not found.";
                return $"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}";
            }
            catch (Exception ex)
            {
                return $"Error retrieving student. {ex.Message}";
            }
        }

        [McpServerTool, Description("Create a new student")]
        public static async Task<string> CreateStudent(StudentsApiClient studentsApiClient, StudentsRequest studentsRequest)
        {
            var createdId = await studentsApiClient.CreateStudent(studentsRequest);
            return createdId == null
                ? "Error creating student."
                : $"Created Student - ID: {createdId}";
        }

        [McpServerTool, Description("Update an existing student")]
        public static async Task<string> UpdateStudent(StudentsApiClient studentsApiClient, UpdateStudentRequest studentsRequest)
        {
            var updatedId = await studentsApiClient.UpdateStudent(studentsRequest);
            return updatedId ? $"Updated Student" : "Error updating student.";
        }

        [McpServerTool, Description("Delete a student by ID")]
        public static async Task<string> DeleteStudent(StudentsApiClient studentsApiClient, Guid id)
        {
            try
            {
                var success = await studentsApiClient.DeleteStudent(id);
                return success ? "Student deleted successfully." : "Error deleting student.";
            }
            catch (Exception ex)
            {
                return $"Error deleting student. {ex.Message}";
            }
        }
    }
}
