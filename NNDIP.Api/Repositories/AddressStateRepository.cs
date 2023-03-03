using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json.Linq;
using NNDIP.Api.Dtos.AddressState;
using NNDIP.Api.Entities;
using NNDIP.Api.Enums;
using NNDIP.Api.NNDIPDbContext;
using NNDIP.Api.Repositories.Interfaces;
using System;
using System.ComponentModel;
using System.Reflection;

namespace NNDIP.Api.Repositories
{
    public class AddressStateRepository : GenericRepository<AddressState>, IAddressStateRepository
    {
        //TODO
        private const string AC_UNIT = "AC unit -";
        private const string AC_UNIT_MODE = "AC unit Mode";
        private const string AC_UNIT_FAN_SPEED = "AC unit Fan Speed";
        private const string RECUPERATION = "Recuperation";
        private const string AC_TEMPERATURE = "AC temperature";

        private List<AddressState> addressStates;
        public AddressStateRepository(NndipDbContext context) : base(context)
        {
            addressStates = new List<AddressState>();
        }

        public AddressStateResult GetAddressStateResult()
        {
            addressStates = _context.AddressStates.ToList();
            return CreateAddressStateResult();
        }

        public async Task<AddressStateResult> GetAddressStateResultAsync()
        {
            addressStates = await _context.AddressStates.ToListAsync();
            return CreateAddressStateResult();
        }

        private AddressStateResult CreateAddressStateResult()
        {
            AddressStateResult addressStateResultDto = new AddressStateResult();
            addressStateResultDto.IsRecuperationOn = IsRecuperationOn(GetAddressStateByActionName(RECUPERATION));
            addressStateResultDto.IsACUnitOn = IsAcUnitOn(GetAddressStateByActionName(AC_UNIT));
            addressStateResultDto.ACUnitMode = SetACUnitMode(GetAddressStateByActionName(AC_UNIT_MODE));
            addressStateResultDto.ACTemperature = SetACTemperature(GetAddressStateByActionName(AC_TEMPERATURE));
            addressStateResultDto.ACUnitFanSpeed = SetACUnitFanSpeed(GetAddressStateByActionName(AC_UNIT_FAN_SPEED));
            if (addressStateResultDto.IsACUnitOn)
            {
                addressStateResultDto.IsFanOn = true;
            }
            else
            {
                addressStateResultDto.IsFanOn = false;
            }
            return addressStateResultDto;
        }

        private string SetACUnitFanSpeed(AddressState? addressState)
        {
            if (addressState is not null)
            {
                if (int.TryParse(addressState.Value, out int result))
                {
                    if (Enum.IsDefined(typeof(ACUnitFanSpeed), result))
                    {
                        return EnumExtender.GetEnumDescription((ACUnitFanSpeed)result);
                    }
                }
            }
            return EnumExtender.GetEnumDescription(ACUnitFanSpeed.NONE);
        }

        private string SetACTemperature(AddressState? addressState)
        {
            if (addressState is not null)
            {
                if (int.TryParse(addressState.Value, out int result))
                {
                    if (Enum.IsDefined(typeof(ACUnitTemperature), result))
                    {
                        return EnumExtender.GetEnumDescription((ACUnitTemperature)result);
                    }
                }
            }
            return EnumExtender.GetEnumDescription(ACUnitTemperature.NONE);
        }

        private string SetACUnitMode(AddressState? addressState)
        {
            if (addressState is not null)
            {
                if (int.TryParse(addressState.Value, out int result))
                {
                    if (Enum.IsDefined(typeof(ACUnitMode), result))
                    {
                        return EnumExtender.GetEnumDescription((ACUnitMode)result);
                    }
                }
            }
            return EnumExtender.GetEnumDescription(ACUnitMode.NONE);
        }

        private bool IsAcUnitOn(AddressState? addressState)
        {
            if (addressState is not null)
            {
                if (int.TryParse(addressState.Value, out int result))
                {
                    if (result == (int)ACUnit.ON)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsRecuperationOn(AddressState? addressState)
        {
            if (addressState is not null)
            {
                if (int.TryParse(addressState.Value, out int result))
                {
                    if (result == (int)Recuperation.ON)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private AddressState? GetAddressStateByActionName(string actionName)
        {
            return addressStates.Find(state => state.ActionName.ToUpper().StartsWith(actionName.ToUpper()));
        }
    }
}
