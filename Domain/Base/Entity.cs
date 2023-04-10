using System.ComponentModel.DataAnnotations.Schema;

namespace DataForwardingWeb.Domain.Base
{
    public abstract class Entity<TId>
    {
        [Column("id")]
        public virtual TId Id { get; set; }
        [Column("is_deleted")]
        public virtual bool IsDeleted { get; set; }
        [Column("created_at")]
        public virtual DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public virtual DateTime? UpdatedAt { get; set; }
        [Column("deteted_at")]
        public virtual DateTime? DeletedAt { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<TId>);
        }

        private static bool IsTransient(Entity<TId> obj)
        {
            return obj != null && Equals(obj.Id, default(TId));
        }
        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(Entity<TId> other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) && !IsTransient(other) && Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                       otherType.IsAssignableFrom(thisType);
            }
            return false;
        }
        public override int GetHashCode()
        {
            if (Equals(Id, default(TId)))
                return base.GetHashCode();
            return Id.GetHashCode();
        }
    }
}