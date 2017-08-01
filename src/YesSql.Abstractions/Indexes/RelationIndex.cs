namespace YesSql.Indexes
{
    public abstract class RelationIndex : MapIndex
    {
        //Id: idalbaran
        public int EntityId { get; set; } //idlineaalb
        public string EntityPath { get; set; } //idlineaalb
        public string EntityPropertyName { get; set; } //PromocionLinea 
        public int TargetDocumentId { get; set; } //idpromocion
        public int TargetEntityId { get; set; } //idpromocionlinea
        //public string TargetPropertyName { get; set; } //producto
    }
}
