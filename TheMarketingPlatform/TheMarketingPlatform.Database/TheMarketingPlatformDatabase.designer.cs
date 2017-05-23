﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TheMarketingPlatform.Database
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="TheMarketingPlatform")]
	public partial class TheMarketingPlatformDatabaseDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Definitionen der Erweiterungsmethoden
    partial void OnCreated();
    partial void InsertLuisEntity(LuisEntity instance);
    partial void UpdateLuisEntity(LuisEntity instance);
    partial void DeleteLuisEntity(LuisEntity instance);
    partial void InsertLuisIntent(LuisIntent instance);
    partial void UpdateLuisIntent(LuisIntent instance);
    partial void DeleteLuisIntent(LuisIntent instance);
    partial void InsertLuisResponse(LuisResponse instance);
    partial void UpdateLuisResponse(LuisResponse instance);
    partial void DeleteLuisResponse(LuisResponse instance);
    partial void InsertMail(Mail instance);
    partial void UpdateMail(Mail instance);
    partial void DeleteMail(Mail instance);
    partial void InsertMailAttachments(MailAttachments instance);
    partial void UpdateMailAttachments(MailAttachments instance);
    partial void DeleteMailAttachments(MailAttachments instance);
    partial void InsertMailReceiver(MailReceiver instance);
    partial void UpdateMailReceiver(MailReceiver instance);
    partial void DeleteMailReceiver(MailReceiver instance);
    #endregion
		
		public TheMarketingPlatformDatabaseDataContext() : 
				base(global::TheMarketingPlatform.Database.Properties.Settings.Default.TheMarketingPlatformConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public TheMarketingPlatformDatabaseDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TheMarketingPlatformDatabaseDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TheMarketingPlatformDatabaseDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TheMarketingPlatformDatabaseDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<LuisEntity> LuisEntity
		{
			get
			{
				return this.GetTable<LuisEntity>();
			}
		}
		
		public System.Data.Linq.Table<LuisIntent> LuisIntent
		{
			get
			{
				return this.GetTable<LuisIntent>();
			}
		}
		
		public System.Data.Linq.Table<LuisResponse> LuisResponse
		{
			get
			{
				return this.GetTable<LuisResponse>();
			}
		}
		
		public System.Data.Linq.Table<Mail> Mail
		{
			get
			{
				return this.GetTable<Mail>();
			}
		}
		
		public System.Data.Linq.Table<MailAttachments> MailAttachments
		{
			get
			{
				return this.GetTable<MailAttachments>();
			}
		}
		
		public System.Data.Linq.Table<MailReceiver> MailReceiver
		{
			get
			{
				return this.GetTable<MailReceiver>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.LuisEntity")]
	public partial class LuisEntity : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _LuisResponseId;
		
		private int _Id;
		
		private string _Entity;
		
		private string _Type;
		
		private System.Nullable<int> _StartIndex;
		
		private System.Nullable<int> _EndIndex;
		
		private double _Score;
		
		private EntityRef<LuisResponse> _LuisResponse;
		
    #region Definitionen der Erweiterungsmethoden
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnLuisResponseIdChanging(int value);
    partial void OnLuisResponseIdChanged();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnEntityChanging(string value);
    partial void OnEntityChanged();
    partial void OnTypeChanging(string value);
    partial void OnTypeChanged();
    partial void OnStartIndexChanging(System.Nullable<int> value);
    partial void OnStartIndexChanged();
    partial void OnEndIndexChanging(System.Nullable<int> value);
    partial void OnEndIndexChanged();
    partial void OnScoreChanging(double value);
    partial void OnScoreChanged();
    #endregion
		
		public LuisEntity()
		{
			this._LuisResponse = default(EntityRef<LuisResponse>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LuisResponseId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int LuisResponseId
		{
			get
			{
				return this._LuisResponseId;
			}
			set
			{
				if ((this._LuisResponseId != value))
				{
					if (this._LuisResponse.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnLuisResponseIdChanging(value);
					this.SendPropertyChanging();
					this._LuisResponseId = value;
					this.SendPropertyChanged("LuisResponseId");
					this.OnLuisResponseIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Entity", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Entity
		{
			get
			{
				return this._Entity;
			}
			set
			{
				if ((this._Entity != value))
				{
					this.OnEntityChanging(value);
					this.SendPropertyChanging();
					this._Entity = value;
					this.SendPropertyChanged("Entity");
					this.OnEntityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Type", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if ((this._Type != value))
				{
					this.OnTypeChanging(value);
					this.SendPropertyChanging();
					this._Type = value;
					this.SendPropertyChanged("Type");
					this.OnTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StartIndex", DbType="Int")]
		public System.Nullable<int> StartIndex
		{
			get
			{
				return this._StartIndex;
			}
			set
			{
				if ((this._StartIndex != value))
				{
					this.OnStartIndexChanging(value);
					this.SendPropertyChanging();
					this._StartIndex = value;
					this.SendPropertyChanged("StartIndex");
					this.OnStartIndexChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EndIndex", DbType="Int")]
		public System.Nullable<int> EndIndex
		{
			get
			{
				return this._EndIndex;
			}
			set
			{
				if ((this._EndIndex != value))
				{
					this.OnEndIndexChanging(value);
					this.SendPropertyChanging();
					this._EndIndex = value;
					this.SendPropertyChanged("EndIndex");
					this.OnEndIndexChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Score", DbType="Float NOT NULL")]
		public double Score
		{
			get
			{
				return this._Score;
			}
			set
			{
				if ((this._Score != value))
				{
					this.OnScoreChanging(value);
					this.SendPropertyChanging();
					this._Score = value;
					this.SendPropertyChanged("Score");
					this.OnScoreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="LuisResponse_LuisEntity", Storage="_LuisResponse", ThisKey="LuisResponseId", OtherKey="Id", IsForeignKey=true)]
		public LuisResponse LuisResponse
		{
			get
			{
				return this._LuisResponse.Entity;
			}
			set
			{
				LuisResponse previousValue = this._LuisResponse.Entity;
				if (((previousValue != value) 
							|| (this._LuisResponse.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._LuisResponse.Entity = null;
						previousValue.LuisEntity.Remove(this);
					}
					this._LuisResponse.Entity = value;
					if ((value != null))
					{
						value.LuisEntity.Add(this);
						this._LuisResponseId = value.Id;
					}
					else
					{
						this._LuisResponseId = default(int);
					}
					this.SendPropertyChanged("LuisResponse");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.LuisIntent")]
	public partial class LuisIntent : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _LuisResponseId;
		
		private int _Id;
		
		private string _Intent;
		
		private double _Score;
		
		private bool _IsTopScore;
		
		private EntityRef<LuisResponse> _LuisResponse;
		
    #region Definitionen der Erweiterungsmethoden
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnLuisResponseIdChanging(int value);
    partial void OnLuisResponseIdChanged();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnIntentChanging(string value);
    partial void OnIntentChanged();
    partial void OnScoreChanging(double value);
    partial void OnScoreChanged();
    partial void OnIsTopScoreChanging(bool value);
    partial void OnIsTopScoreChanged();
    #endregion
		
		public LuisIntent()
		{
			this._LuisResponse = default(EntityRef<LuisResponse>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LuisResponseId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int LuisResponseId
		{
			get
			{
				return this._LuisResponseId;
			}
			set
			{
				if ((this._LuisResponseId != value))
				{
					if (this._LuisResponse.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnLuisResponseIdChanging(value);
					this.SendPropertyChanging();
					this._LuisResponseId = value;
					this.SendPropertyChanged("LuisResponseId");
					this.OnLuisResponseIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Intent", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Intent
		{
			get
			{
				return this._Intent;
			}
			set
			{
				if ((this._Intent != value))
				{
					this.OnIntentChanging(value);
					this.SendPropertyChanging();
					this._Intent = value;
					this.SendPropertyChanged("Intent");
					this.OnIntentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Score", DbType="Float NOT NULL")]
		public double Score
		{
			get
			{
				return this._Score;
			}
			set
			{
				if ((this._Score != value))
				{
					this.OnScoreChanging(value);
					this.SendPropertyChanging();
					this._Score = value;
					this.SendPropertyChanged("Score");
					this.OnScoreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsTopScore", DbType="Bit NOT NULL")]
		public bool IsTopScore
		{
			get
			{
				return this._IsTopScore;
			}
			set
			{
				if ((this._IsTopScore != value))
				{
					this.OnIsTopScoreChanging(value);
					this.SendPropertyChanging();
					this._IsTopScore = value;
					this.SendPropertyChanged("IsTopScore");
					this.OnIsTopScoreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="LuisResponse_LuisIntent", Storage="_LuisResponse", ThisKey="LuisResponseId", OtherKey="Id", IsForeignKey=true)]
		public LuisResponse LuisResponse
		{
			get
			{
				return this._LuisResponse.Entity;
			}
			set
			{
				LuisResponse previousValue = this._LuisResponse.Entity;
				if (((previousValue != value) 
							|| (this._LuisResponse.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._LuisResponse.Entity = null;
						previousValue.LuisIntent.Remove(this);
					}
					this._LuisResponse.Entity = value;
					if ((value != null))
					{
						value.LuisIntent.Add(this);
						this._LuisResponseId = value.Id;
					}
					else
					{
						this._LuisResponseId = default(int);
					}
					this.SendPropertyChanged("LuisResponse");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.LuisResponse")]
	public partial class LuisResponse : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _MailId;
		
		private System.DateTime _TimeStamp;
		
		private EntitySet<LuisEntity> _LuisEntity;
		
		private EntitySet<LuisIntent> _LuisIntent;
		
		private EntityRef<Mail> _Mail;
		
    #region Definitionen der Erweiterungsmethoden
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnMailIdChanging(int value);
    partial void OnMailIdChanged();
    partial void OnTimeStampChanging(System.DateTime value);
    partial void OnTimeStampChanged();
    #endregion
		
		public LuisResponse()
		{
			this._LuisEntity = new EntitySet<LuisEntity>(new Action<LuisEntity>(this.attach_LuisEntity), new Action<LuisEntity>(this.detach_LuisEntity));
			this._LuisIntent = new EntitySet<LuisIntent>(new Action<LuisIntent>(this.attach_LuisIntent), new Action<LuisIntent>(this.detach_LuisIntent));
			this._Mail = default(EntityRef<Mail>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MailId", DbType="Int NOT NULL")]
		public int MailId
		{
			get
			{
				return this._MailId;
			}
			set
			{
				if ((this._MailId != value))
				{
					if (this._Mail.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMailIdChanging(value);
					this.SendPropertyChanging();
					this._MailId = value;
					this.SendPropertyChanged("MailId");
					this.OnMailIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TimeStamp", DbType="DateTime NOT NULL")]
		public System.DateTime TimeStamp
		{
			get
			{
				return this._TimeStamp;
			}
			set
			{
				if ((this._TimeStamp != value))
				{
					this.OnTimeStampChanging(value);
					this.SendPropertyChanging();
					this._TimeStamp = value;
					this.SendPropertyChanged("TimeStamp");
					this.OnTimeStampChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="LuisResponse_LuisEntity", Storage="_LuisEntity", ThisKey="Id", OtherKey="LuisResponseId")]
		public EntitySet<LuisEntity> LuisEntity
		{
			get
			{
				return this._LuisEntity;
			}
			set
			{
				this._LuisEntity.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="LuisResponse_LuisIntent", Storage="_LuisIntent", ThisKey="Id", OtherKey="LuisResponseId")]
		public EntitySet<LuisIntent> LuisIntent
		{
			get
			{
				return this._LuisIntent;
			}
			set
			{
				this._LuisIntent.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Mail_LuisResponse", Storage="_Mail", ThisKey="MailId", OtherKey="Id", IsForeignKey=true)]
		public Mail Mail
		{
			get
			{
				return this._Mail.Entity;
			}
			set
			{
				Mail previousValue = this._Mail.Entity;
				if (((previousValue != value) 
							|| (this._Mail.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Mail.Entity = null;
						previousValue.LuisResponse.Remove(this);
					}
					this._Mail.Entity = value;
					if ((value != null))
					{
						value.LuisResponse.Add(this);
						this._MailId = value.Id;
					}
					else
					{
						this._MailId = default(int);
					}
					this.SendPropertyChanged("Mail");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_LuisEntity(LuisEntity entity)
		{
			this.SendPropertyChanging();
			entity.LuisResponse = this;
		}
		
		private void detach_LuisEntity(LuisEntity entity)
		{
			this.SendPropertyChanging();
			entity.LuisResponse = null;
		}
		
		private void attach_LuisIntent(LuisIntent entity)
		{
			this.SendPropertyChanging();
			entity.LuisResponse = this;
		}
		
		private void detach_LuisIntent(LuisIntent entity)
		{
			this.SendPropertyChanging();
			entity.LuisResponse = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Mail")]
	public partial class Mail : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _From;
		
		private string _Subject;
		
		private string _Body;
		
		private System.DateTime _TimeStamp;
		
		private EntitySet<LuisResponse> _LuisResponse;
		
		private EntitySet<MailAttachments> _MailAttachments;
		
		private EntitySet<MailReceiver> _MailReceiver;
		
    #region Definitionen der Erweiterungsmethoden
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnFromChanging(string value);
    partial void OnFromChanged();
    partial void OnSubjectChanging(string value);
    partial void OnSubjectChanged();
    partial void OnBodyChanging(string value);
    partial void OnBodyChanged();
    partial void OnTimeStampChanging(System.DateTime value);
    partial void OnTimeStampChanged();
    #endregion
		
		public Mail()
		{
			this._LuisResponse = new EntitySet<LuisResponse>(new Action<LuisResponse>(this.attach_LuisResponse), new Action<LuisResponse>(this.detach_LuisResponse));
			this._MailAttachments = new EntitySet<MailAttachments>(new Action<MailAttachments>(this.attach_MailAttachments), new Action<MailAttachments>(this.detach_MailAttachments));
			this._MailReceiver = new EntitySet<MailReceiver>(new Action<MailReceiver>(this.attach_MailReceiver), new Action<MailReceiver>(this.detach_MailReceiver));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[From]", Storage="_From", DbType="NVarChar(254) NOT NULL", CanBeNull=false)]
		public string From
		{
			get
			{
				return this._From;
			}
			set
			{
				if ((this._From != value))
				{
					this.OnFromChanging(value);
					this.SendPropertyChanging();
					this._From = value;
					this.SendPropertyChanged("From");
					this.OnFromChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Subject", DbType="NVarChar(254)")]
		public string Subject
		{
			get
			{
				return this._Subject;
			}
			set
			{
				if ((this._Subject != value))
				{
					this.OnSubjectChanging(value);
					this.SendPropertyChanging();
					this._Subject = value;
					this.SendPropertyChanged("Subject");
					this.OnSubjectChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Body", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Body
		{
			get
			{
				return this._Body;
			}
			set
			{
				if ((this._Body != value))
				{
					this.OnBodyChanging(value);
					this.SendPropertyChanging();
					this._Body = value;
					this.SendPropertyChanged("Body");
					this.OnBodyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TimeStamp", DbType="DateTime NOT NULL")]
		public System.DateTime TimeStamp
		{
			get
			{
				return this._TimeStamp;
			}
			set
			{
				if ((this._TimeStamp != value))
				{
					this.OnTimeStampChanging(value);
					this.SendPropertyChanging();
					this._TimeStamp = value;
					this.SendPropertyChanged("TimeStamp");
					this.OnTimeStampChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Mail_LuisResponse", Storage="_LuisResponse", ThisKey="Id", OtherKey="MailId")]
		public EntitySet<LuisResponse> LuisResponse
		{
			get
			{
				return this._LuisResponse;
			}
			set
			{
				this._LuisResponse.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Mail_MailAttachments", Storage="_MailAttachments", ThisKey="Id", OtherKey="MailId")]
		public EntitySet<MailAttachments> MailAttachments
		{
			get
			{
				return this._MailAttachments;
			}
			set
			{
				this._MailAttachments.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Mail_MailReceiver", Storage="_MailReceiver", ThisKey="Id", OtherKey="MailId")]
		public EntitySet<MailReceiver> MailReceiver
		{
			get
			{
				return this._MailReceiver;
			}
			set
			{
				this._MailReceiver.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_LuisResponse(LuisResponse entity)
		{
			this.SendPropertyChanging();
			entity.Mail = this;
		}
		
		private void detach_LuisResponse(LuisResponse entity)
		{
			this.SendPropertyChanging();
			entity.Mail = null;
		}
		
		private void attach_MailAttachments(MailAttachments entity)
		{
			this.SendPropertyChanging();
			entity.Mail = this;
		}
		
		private void detach_MailAttachments(MailAttachments entity)
		{
			this.SendPropertyChanging();
			entity.Mail = null;
		}
		
		private void attach_MailReceiver(MailReceiver entity)
		{
			this.SendPropertyChanging();
			entity.Mail = this;
		}
		
		private void detach_MailReceiver(MailReceiver entity)
		{
			this.SendPropertyChanging();
			entity.Mail = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.MailAttachments")]
	public partial class MailAttachments : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _MailId;
		
		private int _Id;
		
		private System.Data.Linq.Binary _Attachment;
		
		private string _FileExtension;
		
		private EntityRef<Mail> _Mail;
		
    #region Definitionen der Erweiterungsmethoden
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnMailIdChanging(int value);
    partial void OnMailIdChanged();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnAttachmentChanging(System.Data.Linq.Binary value);
    partial void OnAttachmentChanged();
    partial void OnFileExtensionChanging(string value);
    partial void OnFileExtensionChanged();
    #endregion
		
		public MailAttachments()
		{
			this._Mail = default(EntityRef<Mail>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MailId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int MailId
		{
			get
			{
				return this._MailId;
			}
			set
			{
				if ((this._MailId != value))
				{
					if (this._Mail.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMailIdChanging(value);
					this.SendPropertyChanging();
					this._MailId = value;
					this.SendPropertyChanged("MailId");
					this.OnMailIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Attachment", DbType="VarBinary(MAX) NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary Attachment
		{
			get
			{
				return this._Attachment;
			}
			set
			{
				if ((this._Attachment != value))
				{
					this.OnAttachmentChanging(value);
					this.SendPropertyChanging();
					this._Attachment = value;
					this.SendPropertyChanged("Attachment");
					this.OnAttachmentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileExtension", DbType="NVarChar(260)")]
		public string FileExtension
		{
			get
			{
				return this._FileExtension;
			}
			set
			{
				if ((this._FileExtension != value))
				{
					this.OnFileExtensionChanging(value);
					this.SendPropertyChanging();
					this._FileExtension = value;
					this.SendPropertyChanged("FileExtension");
					this.OnFileExtensionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Mail_MailAttachments", Storage="_Mail", ThisKey="MailId", OtherKey="Id", IsForeignKey=true)]
		public Mail Mail
		{
			get
			{
				return this._Mail.Entity;
			}
			set
			{
				Mail previousValue = this._Mail.Entity;
				if (((previousValue != value) 
							|| (this._Mail.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Mail.Entity = null;
						previousValue.MailAttachments.Remove(this);
					}
					this._Mail.Entity = value;
					if ((value != null))
					{
						value.MailAttachments.Add(this);
						this._MailId = value.Id;
					}
					else
					{
						this._MailId = default(int);
					}
					this.SendPropertyChanged("Mail");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.MailReceiver")]
	public partial class MailReceiver : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _MailId;
		
		private string _Receiver;
		
		private EntityRef<Mail> _Mail;
		
    #region Definitionen der Erweiterungsmethoden
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnMailIdChanging(int value);
    partial void OnMailIdChanged();
    partial void OnReceiverChanging(string value);
    partial void OnReceiverChanged();
    #endregion
		
		public MailReceiver()
		{
			this._Mail = default(EntityRef<Mail>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MailId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int MailId
		{
			get
			{
				return this._MailId;
			}
			set
			{
				if ((this._MailId != value))
				{
					if (this._Mail.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMailIdChanging(value);
					this.SendPropertyChanging();
					this._MailId = value;
					this.SendPropertyChanged("MailId");
					this.OnMailIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Receiver", DbType="NVarChar(254) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Receiver
		{
			get
			{
				return this._Receiver;
			}
			set
			{
				if ((this._Receiver != value))
				{
					this.OnReceiverChanging(value);
					this.SendPropertyChanging();
					this._Receiver = value;
					this.SendPropertyChanged("Receiver");
					this.OnReceiverChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Mail_MailReceiver", Storage="_Mail", ThisKey="MailId", OtherKey="Id", IsForeignKey=true)]
		public Mail Mail
		{
			get
			{
				return this._Mail.Entity;
			}
			set
			{
				Mail previousValue = this._Mail.Entity;
				if (((previousValue != value) 
							|| (this._Mail.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Mail.Entity = null;
						previousValue.MailReceiver.Remove(this);
					}
					this._Mail.Entity = value;
					if ((value != null))
					{
						value.MailReceiver.Add(this);
						this._MailId = value.Id;
					}
					else
					{
						this._MailId = default(int);
					}
					this.SendPropertyChanged("Mail");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
