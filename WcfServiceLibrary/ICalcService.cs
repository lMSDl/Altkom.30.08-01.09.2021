using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICalcService" in both code and config file together.
    [ServiceContract]
    public interface ICalcService
    {
        [OperationContract]
        float Add(float a, float b);
        [OperationContract]
        float Substract(float a, float b);
        [OperationContract]
        float Multiply(float a, float b);
        
        float Divide(float a, float b);
    }
}
