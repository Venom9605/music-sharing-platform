﻿using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IArtistRoleRepository : IBaseRepository<DTO.ArtistRole>, IArtistRoleRepositoryCustom
{
    
}

public interface IArtistRoleRepositoryCustom
{
    void CustomMethodTest();
}