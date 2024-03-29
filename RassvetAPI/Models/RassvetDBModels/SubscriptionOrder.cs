﻿using System;
using System.Collections.Generic;

#nullable disable

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class SubscriptionOrder
    {
        public SubscriptionOrder()
        {
            Bills = new HashSet<Bill>();
        }

        public int Id { get; set; }
        public int OfferId { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public bool Confirmed { get; set; }

        public virtual ClientInfo Client { get; set; }
        public virtual Offer Offer { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
    }
}
