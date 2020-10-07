namespace Analystor.Nishomi.Core
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// Base Enitics Exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    /// <seealso cref="System.Runtime.Serialization.ISerializable" />
    [Serializable]
    public class NishomiException : Exception, ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NishomiException"/> class.
        /// </summary>
        public NishomiException()
        {
            this.GenerateExceptionId();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NishomiException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NishomiException(string message)
            : base(message)
        {
            this.GenerateExceptionId();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NishomiException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public NishomiException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.GenerateExceptionId();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NishomiException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        protected NishomiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.GenerateExceptionId();
        }

        /// <summary>
        /// Gets or sets the exception identifier.
        /// </summary>
        /// <value>
        /// The exception identifier.
        /// </value>
        public Guid ExceptionId
        {
            get;
            protected set;
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
        /// </PermissionSet>
        public override string ToString()
        {
            return string.Concat("Exception Id: ", this.ExceptionId, "; ", base.ToString());
        }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
        /// </PermissionSet>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ExceptionId", this.ExceptionId);
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="System.ArgumentNullException">Serialization info.</exception>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
        /// </PermissionSet>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            this.GetObjectData(info, context);
        }

        /// <summary>
        /// Generates the unique identifier.
        /// </summary>
        private void GenerateExceptionId()
        {
            this.ExceptionId = Guid.NewGuid();
        }
    }
}
