﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ENK.ServiceReference2 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference2.LycaAPISoap")]
    public interface LycaAPISoap {
        
        // CODEGEN: Generating message contract since element name HelloWorldResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        ENK.ServiceReference2.HelloWorldResponse HelloWorld(ENK.ServiceReference2.HelloWorldRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        System.Threading.Tasks.Task<ENK.ServiceReference2.HelloWorldResponse> HelloWorldAsync(ENK.ServiceReference2.HelloWorldRequest request);
        
        // CODEGEN: Generating message contract since element name URL from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LycaAPIRequest", ReplyAction="*")]
        ENK.ServiceReference2.LycaAPIRequestResponse LycaAPIRequest(ENK.ServiceReference2.LycaAPIRequestRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LycaAPIRequest", ReplyAction="*")]
        System.Threading.Tasks.Task<ENK.ServiceReference2.LycaAPIRequestResponse> LycaAPIRequestAsync(ENK.ServiceReference2.LycaAPIRequestRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorld", Namespace="http://tempuri.org/", Order=0)]
        public ENK.ServiceReference2.HelloWorldRequestBody Body;
        
        public HelloWorldRequest() {
        }
        
        public HelloWorldRequest(ENK.ServiceReference2.HelloWorldRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class HelloWorldRequestBody {
        
        public HelloWorldRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorldResponse", Namespace="http://tempuri.org/", Order=0)]
        public ENK.ServiceReference2.HelloWorldResponseBody Body;
        
        public HelloWorldResponse() {
        }
        
        public HelloWorldResponse(ENK.ServiceReference2.HelloWorldResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class HelloWorldResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string HelloWorldResult;
        
        public HelloWorldResponseBody() {
        }
        
        public HelloWorldResponseBody(string HelloWorldResult) {
            this.HelloWorldResult = HelloWorldResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class LycaAPIRequestRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="LycaAPIRequest", Namespace="http://tempuri.org/", Order=0)]
        public ENK.ServiceReference2.LycaAPIRequestRequestBody Body;
        
        public LycaAPIRequestRequest() {
        }
        
        public LycaAPIRequestRequest(ENK.ServiceReference2.LycaAPIRequestRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class LycaAPIRequestRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string URL;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Data;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string SOAPAction;
        
        public LycaAPIRequestRequestBody() {
        }
        
        public LycaAPIRequestRequestBody(string URL, string Data, string SOAPAction) {
            this.URL = URL;
            this.Data = Data;
            this.SOAPAction = SOAPAction;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class LycaAPIRequestResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="LycaAPIRequestResponse", Namespace="http://tempuri.org/", Order=0)]
        public ENK.ServiceReference2.LycaAPIRequestResponseBody Body;
        
        public LycaAPIRequestResponse() {
        }
        
        public LycaAPIRequestResponse(ENK.ServiceReference2.LycaAPIRequestResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class LycaAPIRequestResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string LycaAPIRequestResult;
        
        public LycaAPIRequestResponseBody() {
        }
        
        public LycaAPIRequestResponseBody(string LycaAPIRequestResult) {
            this.LycaAPIRequestResult = LycaAPIRequestResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface LycaAPISoapChannel : ENK.ServiceReference2.LycaAPISoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LycaAPISoapClient : System.ServiceModel.ClientBase<ENK.ServiceReference2.LycaAPISoap>, ENK.ServiceReference2.LycaAPISoap {
        
        public LycaAPISoapClient() {
        }
        
        public LycaAPISoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LycaAPISoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LycaAPISoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LycaAPISoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ENK.ServiceReference2.HelloWorldResponse ENK.ServiceReference2.LycaAPISoap.HelloWorld(ENK.ServiceReference2.HelloWorldRequest request) {
            return base.Channel.HelloWorld(request);
        }
        
        public string HelloWorld() {
            ENK.ServiceReference2.HelloWorldRequest inValue = new ENK.ServiceReference2.HelloWorldRequest();
            inValue.Body = new ENK.ServiceReference2.HelloWorldRequestBody();
            ENK.ServiceReference2.HelloWorldResponse retVal = ((ENK.ServiceReference2.LycaAPISoap)(this)).HelloWorld(inValue);
            return retVal.Body.HelloWorldResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ENK.ServiceReference2.HelloWorldResponse> ENK.ServiceReference2.LycaAPISoap.HelloWorldAsync(ENK.ServiceReference2.HelloWorldRequest request) {
            return base.Channel.HelloWorldAsync(request);
        }
        
        public System.Threading.Tasks.Task<ENK.ServiceReference2.HelloWorldResponse> HelloWorldAsync() {
            ENK.ServiceReference2.HelloWorldRequest inValue = new ENK.ServiceReference2.HelloWorldRequest();
            inValue.Body = new ENK.ServiceReference2.HelloWorldRequestBody();
            return ((ENK.ServiceReference2.LycaAPISoap)(this)).HelloWorldAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ENK.ServiceReference2.LycaAPIRequestResponse ENK.ServiceReference2.LycaAPISoap.LycaAPIRequest(ENK.ServiceReference2.LycaAPIRequestRequest request) {
            return base.Channel.LycaAPIRequest(request);
        }
        
        public string LycaAPIRequest(string URL, string Data, string SOAPAction) {
            ENK.ServiceReference2.LycaAPIRequestRequest inValue = new ENK.ServiceReference2.LycaAPIRequestRequest();
            inValue.Body = new ENK.ServiceReference2.LycaAPIRequestRequestBody();
            inValue.Body.URL = URL;
            inValue.Body.Data = Data;
            inValue.Body.SOAPAction = SOAPAction;
            ENK.ServiceReference2.LycaAPIRequestResponse retVal = ((ENK.ServiceReference2.LycaAPISoap)(this)).LycaAPIRequest(inValue);
            return retVal.Body.LycaAPIRequestResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ENK.ServiceReference2.LycaAPIRequestResponse> ENK.ServiceReference2.LycaAPISoap.LycaAPIRequestAsync(ENK.ServiceReference2.LycaAPIRequestRequest request) {
            return base.Channel.LycaAPIRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<ENK.ServiceReference2.LycaAPIRequestResponse> LycaAPIRequestAsync(string URL, string Data, string SOAPAction) {
            ENK.ServiceReference2.LycaAPIRequestRequest inValue = new ENK.ServiceReference2.LycaAPIRequestRequest();
            inValue.Body = new ENK.ServiceReference2.LycaAPIRequestRequestBody();
            inValue.Body.URL = URL;
            inValue.Body.Data = Data;
            inValue.Body.SOAPAction = SOAPAction;
            return ((ENK.ServiceReference2.LycaAPISoap)(this)).LycaAPIRequestAsync(inValue);
        }
    }
}
