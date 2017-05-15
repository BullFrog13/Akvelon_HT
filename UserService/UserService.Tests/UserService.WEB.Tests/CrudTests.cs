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
        private const string ExecPath = "UserService.WEB\\bin\\debug\\UserService.WEB.exe";
        private const string ServiceUri = "http://localhost:5000/UserService/Users";
        private const string StubUserId = "1";
        private const string JsonMediaType = "application/json";

        private Process _serviceProcess;
        private JavaScriptSerializer _jsonSerializer;

        [OneTimeSetUp]
        public void Setup()
        {
            _jsonSerializer = new JavaScriptSerializer();
            var test = Path.Combine(
                Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\")), 
                ExecPath);

            _serviceProcess = Process.Start(test);
        }

        [Test]
        public async Task GetUsersTest()
        {
            HttpResponseMessage responseMessage;

            using (var httpClient = new HttpClient())
            {
                responseMessage = await httpClient.GetAsync(ServiceUri);
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
                var createContent = new StringContent(
                    JsonConvert.SerializeObject(userStub),
                    Encoding.UTF8,
                    JsonMediaType);
                await httpClient.PostAsync(ServiceUri, createContent);

                var getResponse = await httpClient.GetAsync(ServiceUri + "/" + StubUserId);

                Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                await httpClient.DeleteAsync(ServiceUri + "/" + StubUserId);
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
                var createContent = new StringContent(
                    JsonConvert.SerializeObject(userStub),
                    Encoding.UTF8,
                    JsonMediaType);
                var updateContent = new StringContent(
                    JsonConvert.SerializeObject(updatedUserStub),
                    Encoding.UTF8,
                    JsonMediaType);

                await httpClient.PostAsync(ServiceUri, createContent);
                await httpClient.PutAsync(ServiceUri, updateContent);

                var getResponse = await httpClient.GetAsync(ServiceUri + "/" + StubUserId);

                Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                await httpClient.DeleteAsync(ServiceUri + "/" + StubUserId);
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
                var createContent = new StringContent(
                    JsonConvert.SerializeObject(userStub),
                    Encoding.UTF8,
                    JsonMediaType);
                await httpClient.PostAsync(ServiceUri, createContent);
                await httpClient.DeleteAsync(ServiceUri + "/" + StubUserId);

                var getResponse = await httpClient.GetAsync(ServiceUri + "/" + StubUserId);
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