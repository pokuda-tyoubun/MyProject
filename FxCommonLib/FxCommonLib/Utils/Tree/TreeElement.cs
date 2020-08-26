#region ' using 

using System;
using System.Collections;
using System.Text;

#endregion
namespace FxCommonLib.Utils.Tree {
	/// <summary>
	/// 階層構造の要素が持つ特有の機能を提供します。</summary>
	///
	public class TreeElement : IEnumerable,
	                           ICloneable
	{
	
		// constructor 
		#region ' + constructor ( Object ) 
		/// <summary>
		/// TreeElement の新しいインスタンスを Object で初期化します。</summary>
		///
		///     <param name="aValueObject">
		///     要素の値となるオブジェクト。</summary>
		/// 
		/// <exception cref="System.ArgumentNullException">
		/// aValueObject が null です。</exception>
		///
		public TreeElement( Object aValueObject )
		{
			this._valueObject = aValueObject;
			this._childElements = new TreeElementCollection( this );
		}
		#endregion
		#region ' + constructor ( Object, String ) 
		/// <summary>
		/// TreeElement の新しいインスタンスを Object および String で初期化します。</summary>
		///
		///     <param name="aValueObject">
		///     値となるオブジェクト。</summary>
		///     <param name="aName">
		///     要素に関連づけられる名前。</summary>
		/// 
		/// <exception cref="System.ArgumentNullException">
		/// aValueObject または aName が null です。</exception>
		/// <exception cref="System.ArgumentException">
		/// aName が空です。</exception>
		///
		public TreeElement( Object aValueObject, String aName )
		{
			if ( aValueObject == null ) throw new ArgumentNullException( "aValueObject" );
			if ( aName == null ) throw new ArgumentNullException( "aName" );
			if ( aName.Trim().Length == 0 ) throw new ArgumentException( "空です。", "aName" );

			this._valueObject = aValueObject;
			this._name = aName;
			this._childElements = new TreeElementCollection( this );
		}
		#endregion
		#region ' + constructor ( Object, String, TreeElementKind ) 
		/// <summary>
		/// TreeElement の新しいインスタンスを 
		/// Object および String および TreeElementKind で初期化します。</summary>
		///
		/// <exception cref="System.ArgumentNullException">
		/// aValueObject または aName が null です。</exception>
		///
		public TreeElement( Object aValueObject, String aName, TreeElementKind anElementKind )
		{
			if ( aValueObject == null ) throw new ArgumentNullException( "aValueObject" );
			if ( aName == null ) throw new ArgumentNullException( "aName" );
			if ( aName.Trim().Length == 0 ) throw new ArgumentException( "空です。", "aName" );
			if ( anElementKind == TreeElementKind.None ) throw new ArgumentException( "TreeElementKind.None に設定することは出来ません。", "anElementKind" );
			if ( anElementKind == TreeElementKind.Link ) throw new ArgumentException( "TreeElementKind.Link で初期化することは出来ません。", "anElementKind" );

			this._valueObject = aValueObject;
			this._name = aName;
			this._kind = anElementKind;
			this._childElements = new TreeElementCollection( this );
		}
		#endregion
		#region ' + constructor ( Object, TreeElementKink ) 
		/// <summary>
		/// TreeElement の新しいインスタンスを Object および TreeElementKind で初期化します。</summary>
		///
		///     <param name="aValueObject">
		///     値となるオブジェクト。</summary>
		///     <param name="anElementKind">
		///     要素の種類。</summary>
		/// 
		/// <exception cref="System.ArgumentNullException">
		/// aValueObject が null です。</exception>
		///
		public TreeElement( Object aValueObject, TreeElementKind anElementKind )
		{
			if ( aValueObject == null ) throw new ArgumentNullException( "aValueObject" );
			if ( anElementKind == TreeElementKind.None ) throw new ArgumentException( "TreeElementKind.None に設定することは出来ません。", "anElementKind" );
			if ( anElementKind == TreeElementKind.Link ) throw new ArgumentException( "TreeElementKind.Link で初期化することは出来ません。", "anElementKind" );

			this._valueObject = aValueObject;
			this._kind = anElementKind;
			this._childElements = new TreeElementCollection( this );
		}
		#endregion

		
		// field 
		#region ' field 

		internal TreeElement _parent = null;
		private Object _valueObject = null;
		private TreeElementCollection _childElements = null;
		private String _name = null;
		private String _pathSeparator = "/";
		private TreeElementKind _kind = TreeElementKind.Composite;
		private Boolean _isVisibleLinkPrefix = true;
		private ArrayList _structureReferenceElements = new ArrayList();

		#endregion
		
		
		// property 
		#region ' + AllChildElements : TreeElementCollection { get } 
		/// <summary>
		/// 下位階層に存在する全ての子要素のコレクションをを取得します。</summary>
		///
		public TreeElementCollection AllChildElements
		{
			get
			{
				TreeElementCollection childElements = new TreeElementCollection( this );

				foreach ( TreeElement element in this.ChildElements )
				{
					childElements.AddSubElement( element );

					if ( element.HasChild )
					{
						childElements.AddRangeSubElements( element.AllChildElements );
					}
				}

				return childElements;
			}
		}
		#endregion
		#region ' + AllCount : Int32 { get } 
		/// <summary>
		/// この要素も含めた下位階層の子要素全ての数を取得します。</summary>
		///
		public Int32 AllCount
		{
			get { return this.AllChildCount + 1; }
		}
		#endregion
		#region ' + AllChildCount : Int32 { get } 
		/// <summary>
		/// 下位階層の子要素も含めて全ての子要素の数を取得します。</summary>
		///
		public Int32 AllChildCount
		{
			get
			{
				Int32 count = this.ChildCount;

				if ( this.HasChild )
				{
					foreach ( TreeElement element in this.ChildElements )
					{
						if ( element.HasChild )
						{
							count += element.AllChildCount;
						}
					}
				}
				return count;
			}
		}
		#endregion
		#region ' + ChildCount : Int32 { get } 
		/// <summary>
		/// 子要素の数を取得します。</summary>
		///
		public Int32 ChildCount
		{
			get
			{
				if ( this.HasChild )
				{
					return this.ChildElements.Count;
				}
				else
				{
					return 0;
				}
			}
		}
		#endregion
		#region ' + ChildElements : TreeElementCollection { get } 
		/// <summary>
		/// 子要素のコレクションを取得します。</summary>
		///
		public TreeElementCollection ChildElements
		{
			get { return this._childElements; }
		}
		#endregion
		#region ' + Depth : Int32 { get } 
		/// <summary>
		/// 0 から始まるこの要素の深さを表す数を取得します。</summary>
		///
		public Int32 Depth
		{
			get
			{
				Int32 depth = 0;

				TreeElement element = this;

				while ( element.IsTop == false )
				{
					depth++;
					element = element.Parent;
				}

				return depth;
			}
		}
		#endregion
		#region ' + FullPath : String { get } 
		/// <summary>
		/// この要素を表すフルパスを取得します。</summary>
		///
		public String FullPath
		{
			get
			{
				ArrayList names = new ArrayList();
				names.Add( this.Name );

				TreeElement currentElement = this;

				Boolean gotRootName = false;

				while ( gotRootName == false )
				{
					if ( currentElement.IsTop == false )
					{
						currentElement = currentElement.Parent;
						names.Add( currentElement.Name );
					}
					else
					{
						gotRootName = true;
					}
				}

				names.Reverse();

				StringBuilder path = new StringBuilder();
				foreach ( String name in names )
				{
					path.Append( name + this.PathSeparator );
				}

				return path.ToString().Remove( path.ToString().Length - this.PathSeparator.Length, this.PathSeparator.Length );
			}
		}
		#endregion
		#region ' + FirstChild : TreeElement { get } 
		/// <summary>
		/// 子要素のはじめの要素を取得します。</summary>
		///
		public TreeElement FirstChild
		{
			get
			{
				if ( this.HasChild )
				{
					return this.ChildElements[ 0 ];
				}
				else
				{
					return null;
				}
			}
		}
		#endregion
		#region ' + HasChild : Boolean { get } 
		/// <summary>
		/// 子要素が存在するかどうかを示す値を取得します。</summary>
		///
		public Boolean HasChild
		{
			get
			{
				if ( this.ChildElements == null ||
				     this.ChildElements.Count == 0 )
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}
		#endregion
		#region ' + HasValue : Boolean { get } 
		/// <summary>
		/// 関連づけられている値となるオブジェクトを持っているかどうかを示す値を取得まします。</summary>
		///
		public Boolean HasValue
		{
			get
			{
				if ( this.Value == null )
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}
		#endregion
		#region ' + IsTop : Boolean { get } 
		/// <summary>
		/// 要素が最上位の階層に存在するかどうかを示す値を取得まします。</summary>
		///
		public Boolean IsTop
		{
			get
			{
				if ( this.Parent == null )
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		#endregion
		#region ' + IsVisibleLinkPrefix : Boolean { get,set } 
		/// <summary>
		/// 要素の種類が Link の場合に表示するリンクを表すプリフィックスを
		/// 表示するかどうかを示す値を取得または設定します。
		/// 既定値は true です。</summary>
		///
		public Boolean IsVisibleLinkPrefix
		{
			get { return _isVisibleLinkPrefix; }
			set { _isVisibleLinkPrefix = value; }
		}
		#endregion
		#region ' + Index : Int32 { get } 
		/// <summary>
		/// 親要素から見たこの要素の 0 から始まるインデックス位置を取得します。
		/// 要素が最上位の階層に位置する場合は 0 を返します。</summary>
		///
		public Int32 Index
		{
			get
			{
				if ( this.IsTop )
				{
					return 0;
				}
				else
				{
					return Parent.ChildElements.IndexOf( this );
				}
			}
		}
		#endregion
		#region ' + IndentedPath : String { get } 
		/// <summary>
		/// 親要素の名前でインデントしたこの要素のパスを取得します。</summary>
		///
		public String IndentedPath
		{
			get
			{
				Int32 parentPathLength = 0;

				if ( IsTop )
				{
					return Name;
				}

				TreeElement parent = Parent;

				while ( parent.IsTop == false )
				{
					parentPathLength += parent.Name.Length;
					parent = parent.Parent;
				}

				parentPathLength += parent.Name.Length;
				
				String indent = new String( ' ', parentPathLength );

				return indent + Name;
			}
		}
		#endregion
		#region ' + IsFirstChild : Boolean { get } 
		/// <summary>
		/// 親要素からみてこの要素が最初の要素かどうかを示す値を取得まします。</summary>
		///
		public Boolean IsFirstChild
		{
			get
			{
				if ( this.IsTop )
				{
					throw new InvalidOperationException( "ルート要素にこの操作は使用できません。" );
				}

				if ( this.Index == 0 )
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		#endregion
		#region ' + IsLastChild : Boolean { get } 
		/// <summary>
		/// 親要素からみてこの要素が最後の要素かどうかを示す値を取得まします。</summary>
		///
		public Boolean IsLastChild
		{
			get
			{
				if ( this.IsTop )
				{
					throw new InvalidOperationException( "ルート要素にこの操作は使用出来ません。" );
				}
				if ( this.Index ==
				     Parent.ChildCount - 1 )
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		#endregion
		#region ' + Kind : TreeElementKind { get } 
		/// <summary>
		/// 要素の種類を取得します。
		/// 既定値は Composite です。</summary>
		///
		public TreeElementKind Kind
		{
			get { return _kind; }
		}
		#endregion
		#region ' + LastChild : TreeElement { get } 
		/// <summary>
		/// 最後尾の子要素を取得します。</summary>
		///
		public TreeElement LastChild
		{
			get
			{
				if ( this.HasChild )
				{
					return this.ChildElements[ this.ChildElements.Count - 1 ];
				}
				else
				{
					return null;
				}
			}
		}
		#endregion
		#region ' + Name : String { get,set } 
		/// <summary>
		/// 要素の名前を取得または設定します。
		/// 取得の際に値が設定されていない場合、Value プロパティに設定されている
		/// Object の ToString メソッドから値を返します。
		/// Value プロパティの Object が null 参照の場合、
		/// Kind プロパティの TreeElementKind 列挙型の ToString メソッドから
		/// 値を返します。</summary>
		///
		public String Name
		{
			get
			{
				String name = "";

				if ( this.Kind == TreeElementKind.Link )
				{
					
					if ( this.IsVisibleLinkPrefix )
					{
						name = "<Link>";
					}

					if ( this._name == null || this._name.Trim().Length == 0 )
					{
						TreeElement linkElement = this.Value as TreeElement;

						if ( linkElement != null )
						{
							name += linkElement.Name;
						}
					}
				}
				else
				{
					if ( this._name == null || this._name.Trim().Length == 0 )
					{
						if ( this.Value != null )
						{
							name = this.Value.ToString();
						}
					}
					else
					{
						name = this._name;
					}

				}

			return name;
				
			}
			set
			{
				if ( value == null ) throw new ArgumentNullException( "value" );
				if ( value.Trim().Length == 0 ) throw new ArgumentException( "空です。", "value" );

				this._name = value;
			}
		}
		#endregion
		#region ' + NextSibling : TreeElement { get } 
		/// <summary>
		/// 同じ階層にある次の要素を取得します。</summary>
		///
		public TreeElement NextSibling
		{
			get
			{
				if ( this.IsTop )
				{
					throw new InvalidOperationException( "ルート要素にこの操作は使用できません。" );
				}
				else
				{
					if ( this.IsLastChild )
					{
						return null;
					}
					else
					{
						return Parent.ChildElements[ this.Index + 1 ];
					}
				}
			}
		}
		#endregion
		#region ' + Parent : TreeElement { get } 
		/// <summary>
		/// 親要素を取得します。
		/// 値の設定は TreeElement クラス内でのみ使用します。</summary>
		///
		public TreeElement Parent
		{
			get { return this._parent; }
		}
		#endregion
		#region ' + PathSeparator : String { get,set } 
		/// <summary>
		/// パスの階層の区切りを表す文字列を取得または設定します。
		/// 既定値は "/" です。</summary>
		///
		public String PathSeparator
		{
			get { return this._pathSeparator; }
			set
			{
				if ( value == null ) throw new ArgumentNullException( "value" );
				if ( value.Trim().Length == 0 ) throw new ArgumentException( "空です。", "value" );

				if ( value != this._pathSeparator )
				{
					this._pathSeparator = value;
				}
			}
		}
		#endregion
		#region ' + PreviousSibling : TreeElement { get } 
		/// <summary>
		/// 同じ階層にある前の要素を取得します。</summary>
		///
		public TreeElement PreviousSibling
		{
			get
			{
				if ( this.IsTop )
				{
					throw new InvalidOperationException( "ルート要素にこの操作は使用できません。" );
				}
				else
				{
					if ( this.IsFirstChild )
					{
						return null;
					}
					else
					{
						return Parent.ChildElements[ this.Index - 1 ];
					}
				}
			}
		}
		#endregion
		#region ' + Top : TreeElement { get } 
		/// <summary>
		/// 最上位の要素を取得します。</summary>
		///
		public TreeElement Top
		{
			get
			{
				TreeElement element = this;

				while ( element.IsTop == false )
				{
					element = element.Parent;
				}

				return element;
			}
		}
		#endregion
		#region ' + Value : Object { get,set } 
		/// <summary>
		/// 要素に関連づけられているオブジェクトを取得します。</summary>
		///
		public Object Value
		{
			get { return _valueObject; }
			set
			{
				if ( value == null ) throw new ArgumentNullException( "value" );

				_valueObject = value;
			}
		}
		#endregion
		#region '   ~ StructureReferenceElements : ArrayList { get } 
		/// <summary>
		/// この要素の階層内で参照されている TreeElement のコレクションを取得します。
		/// クライアントからこのメンバにアクセスしないでください。</summary>
		///
		internal ArrayList StructureReferenceElements
		{
			get
			{
				if ( this.IsTop )
				{
					return this._structureReferenceElements;
				}
				else
				{
					return this.Top.StructureReferenceElements;
				}

			}
		}
		#endregion
		
		
		// method 
		#region ' + Add ( TreeElement ) : void 
		/// <summary>
		/// 指定した要素を子要素へ追加します。</summary>
		///
		public void Add( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException( "anElement" );
			if ( this.Kind != TreeElementKind.Composite ) throw new InvalidOperationException( "種類が Composite 以外の要素に子要素を追加することは出来ません。" );
			if ( this.IsContainsStructure( anElement ) ) throw new InvalidOperationException( "追加しようとした要素は既にこの要素を含む階層内に存在しています。" );

			this.StructureReferenceElements.Add( anElement );
			anElement.SetParentElement( this );
			this._childElements.Add( anElement );
			
		}
		#endregion
		#region ' + AddElements ( TreeElementCollection ) : void 
		/// <summary>
		/// 要素のコレクションを子要素へ追加します。</summary>
		///
		public void AddElements( TreeElementCollection someElements )
		{
			if ( someElements == null ) throw new ArgumentNullException( "someElements" );

			foreach ( TreeElement element in someElements )
			{
				this.Add( element );
			}
		}
		#endregion
		#region ' + AddPrependChild ( TreeElement ) : void 
		/// <summary>
		/// この要素の子要素の先頭へ指定した要素を挿入します。</summary>
		///
		public void AddPrependChild( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException( "anElement" );
			if ( Kind != TreeElementKind.Composite ) throw new InvalidOperationException( "種類が Composite 以外の要素へ子要素を追加することは出来ません。" );
			if ( this.IsContainsStructure( anElement ) ) throw new InvalidOperationException( "追加しようとした要素は既にこの要素を含む階層内に存在しています。" );

			if ( this.HasChild )
			{
				this.StructureReferenceElements.Add( anElement );
				anElement.SetParentElement( this );
				this.ChildElements.Insert( 0, anElement );
			}
			else
			{
				this.Add( anElement );
			}
		}
		#endregion
		#region ' + Clone () : Object 
		/// <summary>
		/// TreeElement のディープコピーを取得します。</summary>
		///
		public Object Clone()
		{
			TreeElement cloneElement = new TreeElement( Value, Name, Kind );

			if ( HasChild )
			{
				foreach ( TreeElement element in ChildElements )
				{
					TreeElement newSubElement = new TreeElement( element.Value, element.Name, element.Kind );
					cloneElement.Add( newSubElement );

					if ( element.HasChild )
					{
						TreeElement childElement = ( TreeElement )element.Clone();
						newSubElement.AddElements( childElement.ChildElements );
					}
				}
			}

			return cloneElement;
		}
		#endregion
		#region ' + CreateLinkElement () : TreeElement 
		/// <summary>
		/// この要素のリンクを生成して取得します。</summary>
		///
		public TreeElement CreateLinkElement()
		{
			
			TreeElement linkElement;
			
			if ( this._name == null )
			{
				linkElement = new TreeElement( this );
				linkElement.SetLinkKind();
			}
			else
			{
				linkElement = new TreeElement( this, this._name );
				linkElement.SetLinkKind();
			}

			return linkElement;
		}
		#endregion
		#region ' + CopyTo ( TreeElement ) : void 
		/// <summary>
		/// この要素を指定した要素の子要素としてコピーします。</summary>
		///
		public void CopyTo( TreeElement aTargetElement )
		{
			if ( aTargetElement == null ) throw new ArgumentNullException( "aTargetElement" );

			aTargetElement.Add( ( TreeElement )this.Clone() );
		}
		#endregion
		#region ' + GetEnumerator () : IEnumerator 
		/// <summary>
		/// コレクションを反復処理できる列挙子を返します。</summary>
		/// 
		/// <returns>
		/// コレクションを反復処理するために使用できる <see cref="T:System.Collections.IEnumerator"/>。</returns>
		/// 
		public IEnumerator GetEnumerator()
		{
			return new TreeElementEnumerator( this.AllChildElements );
		}
		#endregion
		#region ' + InsertAfter ( TreeElement ) : void 
		/// <summary>
		/// 同じ階層でこの要素の直後に指定した要素を挿入します。</summary>
		///
		public void InsertAfter( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException( "anElement" );
			if ( this.IsTop ) throw new InvalidOperationException( "ルート要素の前後に要素を挿入することは出来ません。" );
			if ( this.IsContainsStructure( anElement	) ) throw new InvalidOperationException( "追加しようとした要素は既にこの要素を含む構造内に含まれています。" );

			if ( this.IsLastChild )
			{
				
				Parent.Add( anElement );
			}
			else
			{
				this.StructureReferenceElements.Add( anElement );
				anElement.SetParentElement( this.Parent );
				this.Parent.ChildElements.Insert( this.Index + 1, anElement );
			}
			
		}
		#endregion
		#region ' + InsertBefore ( TreeElement ) : void 
		/// <summary>
		/// 同じ階層でこの要素の直前に指定した要素を挿入します。</summary>
		///
		public void InsertBefore( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException( "anElement" );
			if ( this.IsTop ) throw new InvalidOperationException( "ルート要素の前後に要素を挿入することは出来ません。" );
			if ( this.IsContainsStructure( anElement	) ) throw new InvalidOperationException( "追加しようとした要素は既にこの要素を含む構造内に含まれています。" );

			if ( this.Index == 0 )
			{
				this.StructureReferenceElements.Add( anElement );
				anElement.SetParentElement( this.Parent );
				this.Parent.ChildElements.Insert( 0, anElement );
			}
			else
			{
				this.StructureReferenceElements.Add( anElement );
				anElement.SetParentElement( this.Parent );
				this.Parent.ChildElements.Insert( this.Index, anElement );
			}
			
		}
		#endregion
		#region ' + IsContains ( TreeElement ) : Boolean 
		/// <summary>
		/// TreeElement が子要素に含まれているか、
		/// またはこの要素と一致するかどうか示す値を取得します。</summary>
		///
		public Boolean IsContains( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException( "anElement" );

			foreach ( TreeElement element in this )
			{
				if ( element.Equals( anElement ) )
				{
					return true;
				}
			}

			return false;
		}
		#endregion
		#region ' + IsContainsStructure( TreeElement ) : Boolean 
		/// <summary>
		/// 指定した TreeElement がこの要素を含む構造内に含まれているかどうかを示す値を取得します。</summary>
		///
		public Boolean IsContainsStructure ( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException( "anElement" );

			return this.StructureReferenceElements.Contains( anElement );

		}
		#endregion
		#region ' + MoveTo ( TreeElement ) : void 
		/// <summary>
		/// この要素を指定した要素の子要素として移動します。</summary>
		///
		public void MoveTo( TreeElement aTargetElememnt )
		{
			if ( aTargetElememnt == null ) throw new ArgumentNullException( "aTargetElememnt" );
			if ( this.IsContains( aTargetElememnt ) ) throw new InvalidOperationException( "移動先の要素はこの要素の子要素です。またはこの要素自身です。" );

			TreeElement moveElement = this;
			this.Remove();
			aTargetElememnt.Add( moveElement );
			
		}
		#endregion
		#region ' + Remove () : void 
		/// <summary>
		/// この要素を親要素から削除します。</summary>
		///
		/// <exception cref="System.InvalidOperationException">
		/// ルート要素を削除することは出来ません。</exception>
		/// 
		public void Remove()
		{
			if ( this.IsTop ) throw new InvalidOperationException( "トップ要素を削除することは出来ません。" );

			this.Parent.RemoveChild( this );
			this.Top.StructureReferenceElements.Remove( this );
			
		}
		#endregion
		#region ' + RemoveAllChild () : void 
		/// <summary>
		/// 子要素を全て削除します。</summary>
		///
		public void RemoveAllChild()
		{
			if ( Kind != TreeElementKind.Composite ) throw new InvalidOperationException( "種類が Composite 以外の要素にこの操作は使用できません。" );

			if ( this.HasChild )
			{
				foreach ( TreeElement childElement in this.ChildElements )
				{
					this.RemoveChild( childElement );
				}
			}
		}
		#endregion
		#region ' + RemoveChild ( TreeElement ) : void 
		/// <summary>
		/// 指定した子要素を削除します。
		/// 指定した要素が子要素に存在しない場合、この処理は無視されます。</summary>
		///
		public void RemoveChild( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException( "anElement" );
			if ( Kind != TreeElementKind.Composite ) throw new InvalidOperationException( "種類が Composite 以外の要素にこの操作は使用できません。" );

			if ( this.ChildElements.IsContains( anElement ) )
			{
				this.ChildElements.Remove( anElement );
				this.Top.StructureReferenceElements.Remove( anElement );
			}
		}
		#endregion
		#region '   ~ SetParentElement ( TreeElement ) : void 
		/// <summary>
		/// この要素の親要素を設定します。
		/// クライアントからこのメンバへアクセスしないでください。</summary>
		///
		internal void SetParentElement ( TreeElement aParentElement )
		{
			if ( aParentElement == null ) throw new ArgumentNullException ( "aParentElement" );
 
			this._parent = aParentElement;
 
		}
		#endregion
		#region '   # SetLinkKind () : void 
		/// <summary>
		/// この要素の種類をリンク要素として設定します。
		/// クライアントからこのメンバへアクセスしないでください。</summary>
		///
		protected internal void SetLinkKind()
		{
			this._kind = TreeElementKind.Link;
		}
		#endregion


		// innertype 
		#region '   - TreeElementEnumerator : IEnumerator 
		/// <summary>
		/// コレクションに対する単純な反復処理をサポートします。</summary>
		///
		private class TreeElementEnumerator : IEnumerator
		{
            
			// constructor 
			#region ' + constructor ( TreeElementCollection ) 
			/// <summary>
			/// 新しい TreeElementEnumerator のインスタンスを TreeElementCollection で初期化します。</summary>
			/// 
			public TreeElementEnumerator( TreeElementCollection aList )
			{
				_list = aList;
			}
			#endregion
            
            
			// field 
			#region ' field 
            
			private TreeElementCollection _list = null;
			private Int32 _currentIndex = -2;
            
			#endregion
            
            
			// property 
			#region ' + Current : Object { get } 
			/// <summary>
			/// コレクション内の現在の要素を取得します。</summary>
			/// 
			public Object Current
			{
				get
				{
					if ( _currentIndex == -1 )
					{
						return _list[ 0 ].Parent;
					}
					else
					{
						return _list[ _currentIndex ];
					}
				}
			}
			#endregion
            
            
			// method 
			#region ' + Reset () : void 
			/// <summary>
			/// コレクションの最初の要素の前に設定します。</summary>
			/// 
			public void Reset()
			{
				_currentIndex = -2;
			}
			#endregion
			#region ' + MoveNext () : Boolean 
			/// <summary>
			/// 列挙子をコレクションの次の要素に進めます。</summary>
			/// 
			public Boolean MoveNext()
			{
				_currentIndex ++;

				if ( _list.Count == 0 ||
				     _list.Count == _currentIndex )
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			#endregion
			
		}
		#endregion

	}
}
