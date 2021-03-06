//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineStore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Purchase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Purchase()
        {
            this.Recipients = new HashSet<Recipient>();
            this.Recurrences = new HashSet<Recurrence>();
            this.ScheduledServices = new HashSet<ScheduledService>();
        }
    
        public int Id { get; set; }
        public Nullable<int> PurchaseId { get; set; }
        public string ServiceName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> RecurrenceId { get; set; }
        public Nullable<int> CustomizationId { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateLastModified { get; set; }
        public string TrackingNumber { get; set; }
        public Nullable<decimal> ShippingAndHandling { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> SubTotal { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string Month { get; set; }
        public string Date { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string AMPM { get; set; }
        public string Minutes { get; set; }
    
        public virtual Customization Customization { get; set; }
        public virtual Recurrence Recurrence { get; set; }
        public virtual Service Service { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recipient> Recipients { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recurrence> Recurrences { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduledService> ScheduledServices { get; set; }
    }
}
