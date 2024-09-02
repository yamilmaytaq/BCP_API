using AutoMapper;
using BCP_API_JM.Models;
using BCP_API_JM.Models.DTO;
using static System.Net.Mime.MediaTypeNames;

namespace BCP_API_JM
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<BD_CLIENTES, BD_CLIENTES_DTO>().ReverseMap();

            CreateMap<BD_CLIENTES, BD_CLIENTES_CREATE_DTO>().ReverseMap();

            CreateMap<BD_CLIENTES, BD_CLIENTES_UPDATE_DTO>().ReverseMap();

            CreateMap<BD_USUARIOS, BD_USUARIOS_DTO>().ReverseMap();

            CreateMap<BD_USUARIOS, BD_USUARIOS_CREATE_DTO>().ReverseMap();

            CreateMap<BD_USUARIOS, BD_USUARIOS_UPDATE_DTO>().ReverseMap();
        }
    }
}
