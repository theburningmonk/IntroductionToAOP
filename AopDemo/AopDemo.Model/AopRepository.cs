namespace AopDemo.Model {
    using System;
    using Attributes;    
    using Entities;
    using PostSharp.Extensibility;

    /// <summary>
    /// Simple repository using AOP attributes
    /// </summary>        
    //// all public methods starting with 'Get'
    //[CheckParameters(AttributeTargetElements = MulticastTargets.Method,
    //                 AttributeTargetMemberAttributes = MulticastAttributes.Public,
    //                 AttributeTargetMembers = "Get*",
    //                 AspectPriority = 2)]
    //// don't apply to property getter and setters
    //[Trace(AttributeExclude = true,
    //       AttributeTargetMembers = "regex:get_.*|set_.*",
    //       AspectPriority = 0)]
    //// apply to all methods
    //[Trace(AttributeTargetElements = MulticastTargets.Method,
    //       AspectPriority = 1)]
    public class AopRepository : IRepository {
        public AopRepository(IDataAccessLayer dal) {
            Dal = dal;
        }

        protected IDataAccessLayer Dal { get; set; }

        //[CheckParameters(AspectPriority = 2)]
        //[Trace(AspectPriority = 1)]
        public Book GetBookById([NotDefaultGuid] Guid id) {
            return Dal.Get<Book>(id);
        }

        //[CheckParameters(AspectPriority = 2)]
        //[Trace(AspectPriority = 1)]
        public Author GetAuthorById([NotDefaultGuid] Guid id) {
            return Dal.Get<Author>(id);
        }
    }
}