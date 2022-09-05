﻿using LibHouse.Business.Application.Users.Inputs;
using LibHouse.Business.Application.Users.Outputs;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Users.Interfaces
{
    public interface IUserLogout
    {
        Task<OutputUserLogout> ExecuteAsync(InputUserLogout input);
    }
}