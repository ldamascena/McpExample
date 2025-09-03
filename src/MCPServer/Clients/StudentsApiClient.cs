using MCPServer.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MCPServer.Clients
{
    public class StudentsApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;

        public StudentsApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<StudentsResponse>> GetStudents()
        {
            try
            {
                var response = await _httpClient.GetAsync("students");
                if (!response.IsSuccessStatusCode)
                    return new List<StudentsResponse>();

                var students = await response.Content.ReadFromJsonAsync<List<StudentsResponse>>(_serializerOptions);
                return students ?? new List<StudentsResponse>();
            }
            catch
            {
                return new List<StudentsResponse>();
            }
        }

        public async Task<StudentsResponse> GetStudent(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"students/{id}");
                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadFromJsonAsync<StudentsResponse>(_serializerOptions);
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> CreateStudent(StudentsRequest student)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("students/create", student);
                if (!response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Status: {response.StatusCode}, Content: {content}");
                    return null;
                }

                // Retorna apenas o Id em string (removendo as aspas do JSON)
                return (await response.Content.ReadAsStringAsync()).Trim('"');
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateStudent(UpdateStudentRequest request)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("students/update", request);
                if (!response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Status: {response.StatusCode}, Content: {content}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteStudent(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"students/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
