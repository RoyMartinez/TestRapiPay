using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Services
{
    public interface IRecordsServices
    {
        string CreateRecord(RecordRequestDto Request, int UserId);
    }
}
