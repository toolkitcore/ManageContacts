namespace ManageContacts.Entity.Abstractions.Audits;

public interface IFullAuditEntity : ICreationAuditEntity, IModificationAuditEntity, IDeletionAuditEntity
{
}