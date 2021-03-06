﻿/*
 * Copyright (c) 2012-2017 Snowflake Computing Inc. All rights reserved.
 */

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Snowflake.Data.Core
{
    public interface IRestRequest
    {
        JObject post(SFRestRequest postRequest);

        JObject get(SFRestRequest getRequest);

        HttpResponseMessage get(S3DownloadRequest getRequest);
    }

    public class S3DownloadRequest
    {
        internal Uri uri{ get; set; }

        internal string qrmk { get; set; }

        // request timeout in millis
        internal int timeout { get; set; }

        // timeout for each http request 
        internal int httpRequestTimeout { get; set; }

        internal Dictionary<string, string> chunkHeaders { get; set; }
    }

    public class SFRestRequest
    {
        public SFRestRequest()
        {
            sfRestRequestTimeout = -1;

            // default each http request timeout to 16 seconds
            httpRequestTimeout = 16000;
        }

        internal Uri uri { get; set; }

        internal Object jsonBody { get; set;  }

        internal String authorizationToken { get; set; }
        
        // timeout for the whole rest request in millis (adding up all http retry)
        internal int sfRestRequestTimeout { get; set; }
        
        // timeout for each http request 
        internal int httpRequestTimeout { get; set; } 

        public override string ToString()
        {
            return String.Format("SFRestRequest {{url: {0}, request body: {1} }}", uri.ToString(), 
                jsonBody.ToString());
        }
    }

    class AuthnRequest
    {
        [JsonProperty(PropertyName = "data")]
        internal AuthnRequestData data { get; set; }

        public override string ToString()
        {
            return String.Format("AuthRequest {{data: {0} }}", data.ToString());
        }
    }

    class AuthnRequestData
    {
        [JsonProperty(PropertyName = "CLIENT_APP_ID")]
        internal String clientAppId { get; set; }

        [JsonProperty(PropertyName = "CLIENT_APP_VERSION")]
        internal String clientAppVersion { get; set; }

        [JsonProperty(PropertyName = "ACCOUNT_NAME", NullValueHandling = NullValueHandling.Ignore)]
        internal String accountName { get; set; }

        [JsonProperty(PropertyName = "LOGIN_NAME")]
        internal String loginName { get; set; }

        [JsonProperty(PropertyName = "PASSWORD")]
        internal String password { get; set; }

        [JsonProperty(PropertyName = "CLIENT_ENVIRONMENT")]
        internal AuthnRequestClientEnv clientEnv { get; set; }

        public override string ToString()
        {
            return String.Format("AuthRequestData {{ClientAppVersion: {0} , AccountName: {1}, loginName: {2} }}", 
                clientAppVersion, accountName, loginName);
        }
    }

    class AuthnRequestClientEnv
    {
        [JsonProperty(PropertyName = "APPLICATION")]
        internal String application { get; set; }

        [JsonProperty(PropertyName = "OS_VERSION")]
        internal String osVersion { get; set; }
    }

    class QueryRequest
    {
        [JsonProperty(PropertyName = "sqlText")]
        internal string sqlText { get; set; }

        [JsonProperty(PropertyName = "describeOnly")]
        internal bool describeOnly { get; set; }

        [JsonProperty(PropertyName = "bindings")]
        internal Dictionary<string, BindingDTO> parameterBindings { get; set; }
    }

    class QueryCancelRequest
    {
        [JsonProperty(PropertyName = "requestId")]
        internal string requestId { get; set; }
    }

    class RenewSessionRequest
    {
        [JsonProperty(PropertyName = "oldSessionToken")]
        internal string oldSessionToken { get; set; }

        [JsonProperty(PropertyName = "requestType")]
        internal string requestType { get; set; }
    }
}
