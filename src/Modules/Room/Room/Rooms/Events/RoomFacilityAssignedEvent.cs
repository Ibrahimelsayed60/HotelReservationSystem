﻿using Room.Rooms.Models;
using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Rooms.Events
{
    public record RoomFacilityAssignedEvent(RoomFacilities roomFacility):IDomainEvent;
    
}
