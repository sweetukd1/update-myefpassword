﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UpdateMyEFPassword
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="EFComLounge")]
	public partial class UpdatePasswordDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public UpdatePasswordDataContext() : 
				base(global::UpdateMyEFPassword.Properties.Settings.Default.EFComLoungeConnectionString2, mappingSource)
		{
			OnCreated();
		}
		
		public UpdatePasswordDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public UpdatePasswordDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public UpdatePasswordDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public UpdatePasswordDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetUsersToUpdatePasswordTemp")]
		public ISingleResult<GetUsersToUpdatePasswordTempResult> GetUsersToUpdatePasswordTemp()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<GetUsersToUpdatePasswordTempResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.UpdatePasswordByUserIdTemp")]
		public int UpdatePasswordByUserIdTemp([global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserId", DbType="Int")] System.Nullable<int> userId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserName", DbType="NVarChar(512)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Password", DbType="NVarChar(256)")] string password)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userId, userName, password);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetExistingActivityMediaMigration")]
		public ISingleResult<GetExistingActivityMediaMigrationResult> GetExistingActivityMediaMigration([global::System.Data.Linq.Mapping.ParameterAttribute(Name="CalenderEventId", DbType="Int")] System.Nullable<int> calenderEventId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TypeOfOperation", DbType="Int")] System.Nullable<int> typeOfOperation)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calenderEventId, typeOfOperation);
			return ((ISingleResult<GetExistingActivityMediaMigrationResult>)(result.ReturnValue));
		}
	}
	
	public partial class GetUsersToUpdatePasswordTempResult
	{
		
		private int _User_Id;
		
		private System.Guid _MembershipId;
		
		
		public GetUsersToUpdatePasswordTempResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_User_Id", DbType="Int NOT NULL")]
		public int User_Id
		{
			get
			{
				return this._User_Id;
			}
			set
			{
				if ((this._User_Id != value))
				{
					this._User_Id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MembershipId", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid MembershipId
		{
			get
			{
				return this._MembershipId;
			}
			set
			{
				if ((this._MembershipId != value))
				{
					this._MembershipId = value;
				}
			}
		}
	}
	
	public partial class GetExistingActivityMediaMigrationResult
	{
		
		private System.Nullable<System.Guid> _Media_id;
		
		private int _CalendarEvent_id;
		private int _sonarTest;
		
		public GetExistingActivityMediaMigrationResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Media_id", DbType="UniqueIdentifier")]
		public System.Nullable<System.Guid> Media_id
		{
			get
			{
				return this._Media_id;
			}
			set
			{
				if ((this._Media_id != value))
				{
					this._Media_id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CalendarEvent_id", DbType="Int NOT NULL")]
		public int CalendarEvent_id
		{
			get
			{
				return this._CalendarEvent_id;
			}
			set
			{
				if ((this._CalendarEvent_id != value))
				{
					this._CalendarEvent_id = value;
				}
			}
		}
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_sonarTest", DbType="Int NOT NULL")]
		public int SonarTest
		{
			get
			{
				return this._sonarTest;
			}
			set
			{
				if ((this._sonarTest != value))
				{
					this._sonarTest = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
