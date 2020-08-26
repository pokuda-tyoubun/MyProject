#region ' using 

using System;


#endregion
namespace FxCommonLib.Utils.Tree {
	/// <summary>
	/// TreeElement の階層内の要素間を移動し操作する機能を提供します。</summary>
	///
	public class TreeElementVisitor
	{
	
		// constructor 
		#region ' + constructor ( TreeElement ) 
		/// <summary>
		/// TreeElementVisitor の新しいインスタンスを TreeElement で初期化します。</summary>
		///
		/// <exception cref="System.ArgumentNullException">
		/// aSourceElement が null です。</exception>
		///
		public TreeElementVisitor( TreeElement aSourceElement )
		{
			if ( aSourceElement == null ) throw new ArgumentNullException( "aSourceElement" );

			this._currentElement = aSourceElement;
		}
		#endregion

		
		// field 
		#region ' field 

		private TreeElement _currentElement = null;	

		#endregion

		
		// property 
		#region ' + Depth : Int32 { get } 
		/// <summary>
		/// 現在の深さを表す 0 から始まる数を取得します。</summary>
		///
		public Int32 Depth
		{
			get { return this.CurrentElement.Depth; }
		}
		#endregion
		#region ' + CurrentElement : TreeElement { get } 
		/// <summary>
		/// 現在の位置に存在する要素を取得します。</summary>
		///
		public TreeElement CurrentElement
		{
			get { return this._currentElement; }
		}
		#endregion
		#region ' + HasChild : Boolean { get } 
		/// <summary>
		/// 現在の要素に子要素が存在するかどうかを示す値を取得します。</summary>
		///
		public Boolean HasChild
		{
			get { return CurrentElement.HasChild; }
		}
		#endregion
		#region ' + HasNext : Boolean { get } 
		/// <summary>
		/// 次の要素が存在するかどうかを示す値を取得します。</summary>
		///
		public Boolean HasNext
		{
			get
			{
				if ( this.CurrentElement.HasChild )
				{
					return true;
				}

				TreeElement element = this.CurrentElement;

				if ( element.IsLastChild == false )
				{
					return true;
				}

				while ( element.IsLastChild )
				{
					element = element.Parent;

					if ( element.IsTop )
					{
						return false;
					}
				}

				return true;
			}
		}
		#endregion
		#region ' + HasNextSibling : Boolean { get } 
		/// <summary>
		/// 同じ階層に次の要素が存在するかどうかを示す値を取得します。</summary>
		///
		public Boolean HasNextSibling
		{
			get
			{
				if ( CurrentElement.IsLastChild )
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
		#region ' + HasPrevious : Boolean { get } 
		/// <summary>
		/// 前の要素が存在するかどうかを示す値を取得します。</summary>
		///
		public Boolean HasPrevious
		{
			get
			{
				if ( this.CurrentElement.IsTop )
				{
					return false;
				}

				return true;
			}
		}
		#endregion
		#region ' + HasPreviousSibling : Boolean { get } 
		/// <summary>
		/// 同じ階層に前の要素が存在するかどうかを示す値を取得します。</summary>
		///
		public Boolean HasPreviousSibling
		{
			get
			{
				if ( CurrentElement.IsFirstChild )
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
		#region ' + Index : Int32 { get } 
		/// <summary>
		/// 現在の要素の位置を表す 0 からはじまるインデックスを取得します。</summary>
		///
		public Int32 Index
		{
			get { return this.CurrentElement.Index; }
		}
		#endregion
		#region ' + IsTop : Boolean { get } 
		/// <summary>
		/// 現在の要素がトップ要素かどうかを示す値を取得まします。</summary>
		///
		public Boolean IsTop
		{
			get { return this.CurrentElement.IsTop; }
		}
		#endregion
		#region ' + ParentElement : TreeElement { get } 
		/// <summary>
		/// 親要素をを取得します。</summary>
		///
		public TreeElement ParentElement
		{
			get
			{
				if ( this.CurrentElement.IsTop )
					throw new InvalidOperationException( "現在の要素はトップ要素です。" +
					                                     "トップ要素から親要素を取得することは出来ません。" );

				return this.CurrentElement.Parent;
			}
		}
		#endregion
		#region ' + TopElement : TreeElement { get } 
		/// <summary>
		/// トップ階層の要素を取得します。</summary>
		///
		public TreeElement TopElement
		{
			get { return this.CurrentElement.Top; }
		}
		#endregion
		#region ' + Value : Object { get,set } 
		/// <summary>
		/// 現在の位置に存在する要素の値を取得または設定します。</summary>
		///
		public Object Value
		{
			get { return CurrentElement.Value; }
			set { CurrentElement.Value = value; }
		}
		#endregion

		
		// method 
		#region ' + Move ( Int32 ) : void 
		/// <summary>
		/// 指定した回数分、前後の要素へ移動します。</summary>
		///
		public void Move( Int32 aCount )
		{
			if ( aCount < 0 )
			{
				this.MovePrevious( aCount );
			}
			else if ( 0 < aCount )
			{
				MoveNext( aCount );
			}
		}
		#endregion
		#region ' + MoveNext () : void 
		/// <summary>
		/// 次の要素へ移動します。</summary>
		///
		public void MoveNext()
		{
			if ( this.CurrentElement.HasChild )
			{
				this._currentElement = this.CurrentElement.FirstChild;
				return;
			}

			if ( this.CurrentElement.IsLastChild == false )
			{
				this._currentElement = this.CurrentElement.NextSibling;
				return;
			}

			TreeElement element = this.CurrentElement;

			while ( element.IsLastChild )
			{
				element = element.Parent;

				if ( element.IsTop )
				{
					throw new InvalidOperationException( "次の要素は存在しません。" );
				}
			}

			this._currentElement = element.NextSibling;
			
		}
		#endregion
		#region ' + MoveNext ( Int32 ) : void 
		/// <summary>
		/// 指定した回数分次の要素へ移動します。</summary>
		///
		public void MoveNext( Int32 aCount )
		{
			if ( aCount <= -1 ) throw new ArgumentOutOfRangeException( "-1 以下です", "aCount" );

			for ( Int32 i = 0; i < aCount; i++ )
			{
				MoveNext();
			}
		}
		#endregion
		#region ' + MoveNextSibling () : void 
		/// <summary>
		/// 同じ階層に存在する次の要素へ移動します。</summary>
		///
		public void MoveNextSibling ()
		{
			if ( HasNextSibling == false )
			{
				throw new InvalidOperationException( "同じ階層に次の要素は存在しません。" );
			}
			
			_currentElement = CurrentElement.NextSibling;
	
		}
		#endregion
		#region ' + MovePrevious () : void 
		/// <summary>
		/// 前の要素へ移動します。</summary>
		///
		public void MovePrevious()
		{
			if ( this.CurrentElement.IsTop ) throw new InvalidOperationException( "前の要素は存在しません。" );

			if ( this.ParentElement.IsTop &&
			     this.CurrentElement.IsFirstChild )
			{
				this._currentElement = this.CurrentElement.Top;
				return;
			}

			if ( this.CurrentElement.IsFirstChild == false &&
			     this.CurrentElement.IsLastChild == false )
			{
				this._currentElement = this.CurrentElement.PreviousSibling;
				return;
			}

			if ( this.CurrentElement.IsFirstChild )
			{
				this._currentElement = this.ParentElement;
				return;
			}

			if ( this.CurrentElement.IsLastChild )
			{
				this._currentElement = this.CurrentElement.PreviousSibling;
				this.MoveToMostBottom();
				return;
			}
		}
		#endregion
		#region ' + MovePrevious ( Int32 ) : void 
		/// <summary>
		/// 指定した回数分前の要素へ移動します。</summary>
		///
		public void MovePrevious( Int32 aCount )
		{
			if ( aCount <= -1 ) throw new ArgumentOutOfRangeException( "-1 以下です", "aCount" );

			for ( Int32 i = 0; i < aCount; i++ )
			{
				this.MovePrevious();
			}
		}
		#endregion
		#region ' + MovePreviousSibling () : void 
		/// <summary>
		/// 同じ階層に存在する前の要素へ移動します。</summary>
		///
		public void MovePreviousSibling ()
		{
			if ( HasPreviousSibling == false )
			{
				throw new InvalidOperationException( "同じ階層に前の要素は存在しません。" );
			}
			
			_currentElement = CurrentElement.PreviousSibling;
	
		}
		#endregion
		#region ' + MoveToLastChild () : void 
		/// <summary>
		/// 現在の要素の一番最後の位置に存在する子要素へ移動します。</summary>
		///
		public void MoveToLastChild()
		{
			if ( this.CurrentElement.HasChild == false )
			{
				throw new InvalidOperationException( "子要素は存在しません。" );
			}

			this._currentElement = this.CurrentElement.LastChild;
		}
		#endregion
		#region ' + MoveToTop () : void 
		/// <summary>
		/// トップ要素へ移動します。</summary>
		///
		public void MoveToTop()
		{
			if ( this.CurrentElement.IsTop == false )
			{
				this._currentElement = this.CurrentElement.Top;
			}
		}
		#endregion
		#region ' + MoveTo ( TreeElement ) : void 
		/// <summary>
		/// 指定した TreeElment へ移動します。</summary>
		///
		public void MoveTo ( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException ( "anElement" );

			if ( this.TopElement.IsContainsStructure( anElement ) == false )
			{
				throw new InvalidOperationException( "指定した要素は構造内に存在しません。" );
			}
			
			this._currentElement = anElement;
 
		}
		#endregion
		#region ' + MoveUp () : void 
		/// <summary>
		/// 現在の要素の親要素へ移動します。</summary>
		///
		public void MoveUp()
		{
			if ( this.IsTop ) throw new InvalidOperationException( "トップ要素に親要素が存在しないため移動できません。" );

			this._currentElement = this.CurrentElement.Parent;
		}
		#endregion
		#region ' + MoveUp ( Int32 ) : void 
		/// <summary>
		/// 指定した回数分上の階層へ移動します。</summary>
		///
		public void MoveUp( Int32 aCount )
		{
			if ( aCount <= -1 ) throw new ArgumentOutOfRangeException( "-1 以下です", "aCount" );

			for ( Int32 count = 0; count < aCount; count++ )
			{
				MoveUp();
			}
		}
		#endregion
		#region '   - MoveToMostBottom () : void 
		/// <summary>
		/// 現在位置からもっとも深い最後の要素へ移動します。</summary>
		///
		private void MoveToMostBottom()
		{
			while ( this.CurrentElement.HasChild )
			{
				this._currentElement = this.CurrentElement.LastChild;
			}
		}
		#endregion
		
	}
}
