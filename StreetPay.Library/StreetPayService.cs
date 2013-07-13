using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace StreetPay.Library
{
    public class StreetPayService
    {
        RestClient client;

        public StreetPayService()
        {
            client = new RestClient("http://localhost.com:3000");
        }

        private Task<RestResponse<T>> Execute<T>(RestRequest req) where T : new()
        {
            TaskCompletionSource<RestResponse<T>> tcs = new TaskCompletionSource<RestResponse<T>>();

            client.ExecuteAsync(req, (response) =>
            {
                tcs.SetResult((RestResponse<T>)response);
            });

            return tcs.Task;
        }

        public async Task<RestResponse<List<Project>>> GetProjects()
        {
            var req = new RestRequest();
            req.Resource = "project";
            req.RequestFormat = DataFormat.Json;
            req.Method = Method.GET;

            return await Execute<List<Project>>(req);
        }

        public async Task<IRestResponse<Project>> AddProject(Project project)
        {
            var req = new RestRequest();
            req.Resource = "project";
            req.RequestFormat = DataFormat.Json;
            req.Method = Method.POST;
            req.AddObject(project);

            return await Execute<Project>(req);
        }

        public async Task<RestResponse<Project>> UpdateProject(Project project)
        {
            var req = new RestRequest();
            req.Resource = "project";
            req.RequestFormat = DataFormat.Json;
            req.Method = Method.PUT;
            req.AddObject(project);

            return await Execute<Project>(req);
        }

        public async Task<RestResponse<Project>> DeleteProject(Project project)
        {
            var req = new RestRequest();
            req.Resource = "project";
            req.RequestFormat = DataFormat.Json;
            req.Method = Method.DELETE;
            req.AddObject(project);

            return await Execute<Project>(req);
        }

        public async Task<RestResponse<Project>> GetProject(int id)
        {
            var req = new RestRequest();
            req.Resource = "project/{id}";
            req.RequestFormat = DataFormat.Json;
            req.Method = Method.GET;
            req.AddParameter("id", id);

            return await Execute<Project>(req);
        }

        public async Task<RestResponse<Payment>> MakePayment(Project project, int money)
        {
            var req = new RestRequest();
            req.Resource = "project/{id}";
            req.RequestFormat = DataFormat.Json;
            req.Method = Method.GET;
            req.AddParameter("id", project.Id); 
            req.AddObject(money);

            return await Execute<Payment>(req);
        }


    }
}
