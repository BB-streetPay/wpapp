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
            client = new RestClient("http://arcane-forest-9458.herokuapp.com");
        }

        private Task<RestResponse<T>> Execute<T>(RestRequest req) where T : new()
        {
            TaskCompletionSource<RestResponse<T>> tcs = new TaskCompletionSource<RestResponse<T>>();

            client.ExecuteAsync<T>(req, (response) =>
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

        public async Task<RestResponse<Project>> AddProject(Project project)
        {
            var req = new RestRequest();
            req.Resource = "project";

            req.AddHeader("Content-Type", "multipart/form-data; boundary=----------------------------83ff53821b7c");
            req.Method = Method.POST;

            var sb = new StringBuilder();
            add(sb, "projectName", project.Name);
            add(sb, "projectDescription", project.Description);
            add(sb, "projectImageUrl", project.Image);
            
            sb.Append("------------------------------83ff53821b7c--");
            var body = sb.ToString();

            req.AddParameter("multipart/form-data; boundary=----------------------------83ff53821b7c",
                body, ParameterType.RequestBody);


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

        public async Task<RestResponse<Project>> GetProject(string id)
        {
            var req = new RestRequest();
            req.Resource = "project/" + id;
            req.RequestFormat = DataFormat.Json;
            req.Method = Method.GET;
            
            return await Execute<Project>(req);
        }
        string boundary = "--------------------------83ff53821b7c";
        private void add(StringBuilder sb, string name, string val)
        {
            sb.AppendLine("------------------------------83ff53821b7c");
            sb.AppendLine(String.Format("Content-Disposition: form-data; name=\"{0}\"", name));
            sb.AppendLine();
            sb.AppendLine(val);
        }

        public async Task<RestResponse<Payment>> MakePayment(Project project, int money)
        {            
            var req = new RestRequest();
            req.Resource = "payment";
            
            req.AddHeader("Content-Type", "multipart/form-data; boundary=----------------------------83ff53821b7c");
            req.Method = Method.POST;
            
            var sb = new StringBuilder();
            add(sb, "paymentProject", project.Id);
            add(sb, "paymentMoney", money.ToString());
            add(sb, "paymentImageUrl", project.Image);
            add(sb, "paymentNick", "winpho");
            sb.Append("------------------------------83ff53821b7c--");
            var body = sb.ToString();

            req.AddParameter("multipart/form-data; boundary=----------------------------83ff53821b7c",
                body, ParameterType.RequestBody);
            

            return await Execute<Payment>(req);
        }


    }
}
