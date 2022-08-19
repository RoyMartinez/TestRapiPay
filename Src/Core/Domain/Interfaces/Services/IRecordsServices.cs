using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IRecordsServices
    {
        Task<string> CreateRecord(RecordRequestDto Request, int UserId);
    }
}
