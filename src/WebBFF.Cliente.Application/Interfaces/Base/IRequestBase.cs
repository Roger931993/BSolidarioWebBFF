using WebBFF.Cliente.Application.DTOs.Base;

namespace WebBFF.Cliente.Application.Interfaces.Base
{
    public interface IRequestBase
    {
        Guid? IdTraking { get; set; }
        InfoSessionDto? InfoSession { get; set; }
    }
}
