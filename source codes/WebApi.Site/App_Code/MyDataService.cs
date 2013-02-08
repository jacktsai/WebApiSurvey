using System;
using System.Data.Services;
using System.Data.Services.Common;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using WebApi.BLL.Entities;

public class MyDataService : DataService<MyDataModel>
{
    public static void InitializeService(DataServiceConfiguration config)
    {
        config.SetEntitySetAccessRule("*", EntitySetRights.AllRead);
        config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
    }

    protected override void OnStartProcessingRequest(ProcessRequestArgs args)
    {
        base.OnStartProcessingRequest(args);
    }

    protected override void HandleException(HandleExceptionArgs args)
    {
        base.HandleException(args);
    }
}
