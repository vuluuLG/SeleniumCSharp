using Framework.Test.Common.DataObject;
using Framework.Test.Common.Helper;
using Framework.Tests.API.DataObject;
using Framework.Tests.API.Helper;
using RestSharp;
using System;
using System.Net;
using static Framework.Test.Common.Helper.ExtentReportsHelper;

namespace Framework.Tests.API.Service
{
    public class CommandProcessorApi : ApiBase
    {
        #region Properties
        private const string ApiKey = "CommandProcessorApi";
        #endregion

        #region Entities
        private string _id = string.Empty;

        public string QuoteID
        {
            get { return _id; }
        }

        #endregion

        #region Actions
        public CommandProcessorApi(EnvironmentSetting setting, string id = "") : base(ApiKey, setting)
        {
            if (string.IsNullOrEmpty(id))
            {
                Guid g = Guid.NewGuid();
                _id = g.ToString();
            }
            else
            {
                _id = id;
            }
        }

        [ExtentStepNode]
        public IRestResponse ExecuteCommand(string commandName, object cmdPayload)
        {
            var postBody = new CommandProcessorCommand()
            {
                AggregateName = "Quote",
                AggregateId = _id,
                CommandName = commandName,
                CommandData = cmdPayload
            };

            return ExecuteAPI("", postBody);
        }

        [ExtentStepNode]
        public string CreateNewQuote()
        {
            string quoteID = string.Empty;

            var response = ExecuteAPI("CreateNewQuote",
                new CommandData()
                {
                    UserEmail = UserAccount.Email
                }
            );

            if (response.StatusCode == HttpStatusCode.OK)
            {
                quoteID = _id;
            }

            GetLastNode().Info("Quote ID: " + quoteID);

            return quoteID;
        }

        [ExtentStepNode]
        public bool ApproveQuote(string quoteID = null)
        {
            if (!string.IsNullOrEmpty(quoteID))
            {
                _id = quoteID;
            }

            GetLastNode().Info("Approve Quote ID: " + _id);
            var response = ExecuteCommand("ApproveQuote", new CommandData());
            return (response.StatusCode == HttpStatusCode.OK);
        }

        [ExtentStepNode]
        public bool BindQuote(string quoteID = null)
        {
            if (!string.IsNullOrEmpty(quoteID))
            {
                _id = quoteID;
            }

            GetLastNode().Info("Bind Quote ID: " + _id);
            var response = ExecuteCommand("BindQuote", new CommandData());
            return (response.StatusCode == HttpStatusCode.OK);
        }

        private IRestResponse ExecuteAPI(string action, object postBody)
        {
            var node = GetLastNode();
            IRestResponse response = null;

            node.LogRequestBodyInfo(postBody);

            RequestParameter requestParameter = new RequestParameter
            {
                Method = Method.POST,
                OperationPath = action,
                PostBody = postBody
            };

            response = SendHttpCall(requestParameter);

            node.LogRestResponseInfo(response);

            return response;
        }
        #endregion
    }
}
