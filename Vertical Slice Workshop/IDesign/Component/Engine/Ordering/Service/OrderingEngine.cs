﻿using System.Threading.Tasks;
using ServiceModelEx.ServiceFabric;

using IDesign.iFX.Service;
using MethodModelEx.Microservices;

using IDesign.Engine.Ordering.Interface;
using IDesign.Access.Menu.Interface;
using IDesign.Access.Customer.Interface;

#if ServiceModelEx_ServiceFabric
using ServiceModelEx.Fabric;
#else
using System.Fabric;
#endif

namespace IDesign.Engine.Ordering.Service
{
   [ApplicationManifest("IDesign.Microservice.Sales","OrderingEngine")]
   public class OrderingEngine : ServiceBase, IOrderingEngine
   {
      public OrderingEngine(StatelessServiceContext context) : base(context)
      {}

      async Task<MatchResponse> IOrderingEngine.MatchAsync(ItemCriteria critera)
      {
         var customerProxy = Proxy.ForComponent<ICustomerAccess>(this);
         await customerProxy.FilterAsync(new CustomerCriteria());

         var menuProxy = Proxy.ForComponent<IMenuAccess>(this);
         await menuProxy.FilterAsync(new FilterCriteria());

         return new MatchResponse();
      }
   }
}
