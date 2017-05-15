using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using NUnit.Framework;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using UserService.WEB.Models;

namespace UserService.Tests.UserService.WEB.Tests
{
    [TestFixture]
    public class CrudTests
    {
        private Uri _serviceUri;
        private Process _serviceProcess;
        private JavaScriptSerializer _jsonSerializer;

        [OneTimeSetUp]
        public void Setup()
        {
            _serviceUri = new Uri("http://localhost:5000/UserService");
            _jsonSerializer = new JavaScriptSerializer();
            var test = Path.Combine(
                Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\")),
                "UserService.WEB\\bin\\debug\\UserService.WEB.exe");

            _serviceProcess = Process.Start(test);
        }

        [Test]
        public async Task GetUsersTest()
        {
            HttpResponseMessage responseMessage;

            using (var httpClient = new HttpClient())
            {
                responseMessage = await httpClient.GetAsync(_serviceUri);
            }

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task GetUserTest()
        {
            var userStub = new User
            {
                NickName = "Test_NickName",
                FullName = "Test_FullName"
            };

            using (var httpClient = new HttpClient())
            {
                var createContent = new StringContent(JsonConvert.SerializeObject(userStub), Encoding.UTF8,
                    "application/json");
                await httpClient.PostAsync("http://localhost:5000/UserService/Users", createContent);

                var getResponse = await httpClient.GetAsync("http://localhost:5000/UserService/Users/1");

                Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                await httpClient.DeleteAsync("http://localhost:5000/UserService/Users/1");
            }
        }

        [Test]
        public async Task UpdateUserTest()
        {
            var userStub = new User
            {
                NickName = "Test_NickName",
                FullName = "Test_FullName"
            };

            var updatedUserStub = new User
            {
                Id = 1,
                NickName = "Updated_Test_NickName",
                FullName = "Updated_Test_FullName"
            };

            using (var httpClient = new HttpClient())
            {
                var createContent = new StringContent(JsonConvert.SerializeObject(userStub), Encoding.UTF8,
                    "application/json");
                var updateContent = new StringContent(JsonConvert.SerializeObject(updatedUserStub), Encoding.UTF8,
                    "application/json");
                await httpClient.PostAsync("http://localhost:5000/UserService/Users", createContent);
                await httpClient.PutAsync("http://localhost:5000/UserService/Users", updateContent);

                var getResponse = await httpClient.GetAsync("http://localhost:5000/UserService/Users/1");
                //var user = (User)_jsonSerializer.Deserialize(responseResult, typeof(User));

                Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                await httpClient.DeleteAsync("http://localhost:5000/UserService/Users/1");
            }
        }

        [Test]
        public async Task DeleteUserTest()
        {
            var userStub = new User
            {
                NickName = "Test_NickName",
                FullName = "Test_FullName"
            };

            using (var httpClient = new HttpClient())
            {
                var createContent = new StringContent(JsonConvert.SerializeObject(userStub), Encoding.UTF8,
                    "application/json");
                await httpClient.PostAsync("http://localhost:5000/UserService/Users", createContent);
                await httpClient.DeleteAsync("http://localhost:5000/UserService/Users/1");

                var getResponse = await httpClient.GetAsync("http://localhost:5000/UserService/Users/1");
                var responseResult = await getResponse.Content.ReadAsStringAsync();
                var user = (User) _jsonSerializer.Deserialize(responseResult, typeof(User));

                Assert.That(user, Is.Null);
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _serviceProcess.Kill();
        }
    }
}