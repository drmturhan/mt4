using DrMturhan.Server.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrMturhan.Server.DataAccess
{
    public static class ChangeTrackerHelpers
    {
        public static void ConvertStateOfNode(EntityEntryGraphNode node)
        {
            IDurumuOlanNesne entity = (IDurumuOlanNesne)node.Entry.Entity;
            node.Entry.State = ConvertToEFState(entity.Durum);
        }
        private static EntityState ConvertToEFState(NesneDurumu nesneDurumu)
        {
            EntityState efState = EntityState.Unchanged;
            switch (nesneDurumu)
            {
                case NesneDurumu.Added:
                    efState = EntityState.Added;
                    break;
                case NesneDurumu.Modified:
                    efState = EntityState.Modified;
                    break;
                case NesneDurumu.Deleted:
                    efState = EntityState.Deleted;
                    break;
                case NesneDurumu.Unchanged:
                    efState = EntityState.Unchanged;
                    break;
            }
            return efState;
        }
    }
}
