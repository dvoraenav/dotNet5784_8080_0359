﻿using BlApi;
namespace BlImplementation;

internal class ClockImplementation : IClock
{
    private readonly DalApi.IDal _dal=DalApi.Factory.Get;
    public DateTime? GetEndProject()=>_dal.Clock.GetEndProject();
    public DateTime? GetStartProject() => _dal.Clock.GetStartProject();
    public DateTime? SetEndProject(DateTime endProject)=> _dal.Clock.SetEndProject(endProject);
    public DateTime? SetStartProject(DateTime startProject)=> _dal.Clock.SetStartProject(startProject);


}

