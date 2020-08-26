#region ' using 

using System;
using System.Collections;

#endregion
namespace FxCommonLib.Utils.Tree {
	/// <summary>
	/// 必要に応じてサイズが動的に増加する TreeElement 型を管理するコレクションを提供します。</summary>
	///
	public class TreeElementCollection : ICloneable,
	                                     ICollection,
	                                     IEnumerable
	{
        
		// constructor 
		#region '   ~ constructor ( TreeElement ) 
		/// <summary>
		/// TreeElementCollection の新しいインスタンスを TreeElement で初期化します。</summary>
		///
		/// <exception cref="System.ArgumentNullException">
		/// anOwnerElement が null です。</exception>
		///
		internal TreeElementCollection( TreeElement anOwnerElement )
		{
			if ( anOwnerElement == null ) throw new ArgumentNullException( "anOwnerElement" );

			this._ownerElement = anOwnerElement;
		}
		#endregion
        
        
		// field 
		#region ' field 
        
		private ArrayList _list = new ArrayList();
		private TreeElement _ownerElement = null;
        
		#endregion
        
        
		// property
		#region ' + BottomItem : TreeElement { get,set } 
		/// <summary>
		/// コレクションの一番下に存在している TreeElement を取得します。</summary>
		///
		public TreeElement BottomItem
		{
			get { return ( TreeElement )this._list[ 0 ]; }
			set { this._list[ 0 ] = value; }
		}
		#endregion
		#region ' + Capacity : Int32 { get,set } 
		/// <summary>
		/// TreeElementCollection に格納できる要素の数を取得または設定します。</summary>
		///
		public Int32 Capacity
		{
			get { return this._list.Capacity; }
			set { this._list.Capacity = value; }
		}
		#endregion
		#region ' + Count : Int32 { get } 
		/// <summary>
		/// TreeElementCollection に実際に格納されている要素の数を取得します。</summary>
		///
		public Int32 Count
		{
			get { return this._list.Count; }
		}
		#endregion
		#region ' + HasItem : Boolean { get } 
		/// <summary>
		/// TreeElementCollections のコレクション内に要素が存在するかどうかを示す値を取得します。</summary>
		///
		public Boolean HasItem
		{
			get
			{
				if ( 0 < this._list.Count )
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
		#region ' + IsFixedSize : Boolean { get } 
		/// <summary>
		/// TreeElementCollection が固定サイズの場合は true 。それ以外の場合は false 。
		/// 既定値は false です。</summary>
		///
		public Boolean IsFixedSize
		{
			get { return this._list.IsFixedSize; }
		}
		#endregion
		#region ' + IsReadOnly : Boolean { get } 
		/// <summary>
		/// TreeElementCollection が読み取り専用の場合は true 。それ以外の場合は false 。
		/// 既定値は false です。</summary>
		///
		public Boolean IsReadOnly
		{
			get { return this._list.IsReadOnly; }
		}
		#endregion
		#region ' + IsSynchronized : Boolean { get } 
		/// <summary>
		/// TreeElementCollections へのアクセスが
		/// 同期されている (スレッド セーフである) 場合は true 。それ以外の場合は false 。
		/// 既定値は false です。</summary>
		///
		public Boolean IsSynchronized
		{
			get { return this._list.IsSynchronized; }
		}
		#endregion
		#region ' + Item [ Int32 ] : TreeElement { get } 
		/// <summary>
		/// 指定したインデックスにある TreeElement を取得または設定します。</summary>
		///
		public TreeElement this[ Int32 anIndex ]
		{
			get { return ( TreeElement )this._list[ anIndex ]; }
		}
		#endregion
		#region ' + Item [ String ] : TreeElement { get } 
		/// <summary>
		/// コレクション内からはじめに見つかった、
		/// 指定した名前を持つ TreeElement を取得します。
		/// 見つからなければ null を返します。</summary>
		///
		public TreeElement this[ String anElementName ]
		{
			get
			{
				if ( anElementName == null ) throw new ArgumentNullException( "anElementName" );
				if ( anElementName.Trim().Length == 0 ) throw new ArgumentException( "空です。", "anElementName" );
				
				foreach ( TreeElement element in this._list )
				{
					if ( element.Name.Equals( anElementName ) )
					{
						return element;
					}
				}
				
				return null;
				
			}
		}
		#endregion
		#region ' + SyncRoot : Object { get } 
		/// <summary>
		/// TreeElementCollection へのアクセスを同期するための使用できるオブジェクトを取得します。</summary>
		///
		public Object SyncRoot
		{
			get { return this._list.SyncRoot; }
		}
		#endregion
		#region ' + TopItem : TreeElement { get,set } 
		/// <summary>
		/// コレクションの一番上に存在している TreeElement を取得します。</summary>
		///
		public TreeElement TopItem
		{
			get { return ( TreeElement )_list[ this.Count - 1 ]; }
			set { this._list[ this.Count - 1 ] = value; }
		}
		#endregion
		#region '   - OwnerElement : TreeElement { get } 
		/// <summary>
		/// このコレクションを保持している TreeElement を取得します。</summary>
		///
		private TreeElement OwnerElement
		{
			get { return this._ownerElement; }
		}
		#endregion

        
		// method 
		#region ' + IsContaints ( TreeElement ) : Boolean 
		/// <summary>
		/// ある要素が TreeElementCollection 内にあるかどうか判断します。</summary>
		///
		public Boolean IsContains( TreeElement anItem )
		{
			return this._list.Contains( anItem );
		}
		#endregion
		#region ' + Clear () : void 
		/// <summary>
		/// TreeElementCollections から全ての要素を削除します。</summary>
		///
		public void Clear()
		{
			foreach ( TreeElement element in this._list )
			{
				this.Remove( element );
			}
		}
		#endregion
		#region ' + Clone () : Object 
		/// <summary>
		/// TreeElementCollection のディープコピーを作成します。</summary>
		///
		public Object Clone()
		{
			TreeElementCollection cloneElementCollection = new TreeElementCollection( ( TreeElement )this.OwnerElement.Clone() );

			foreach ( TreeElement element in this._list )
			{
				cloneElementCollection.Add( ( TreeElement )element.Clone() );

				if ( element.HasChild )
				{
					cloneElementCollection.AddRange( ( TreeElementCollection )element.ChildElements.Clone() );
				}
			}

			return cloneElementCollection;
		}
		#endregion
		#region ' + CopyTo ( Array ) : void 
		/// <summary>
		/// TreeElementCollections 全体を互換性のある 1 次元の Array にコピーします。
		/// コピー操作は、コピー先の配列の先頭から始まります。</summary>
		/// 
		///    <param name="anArray">
		///     TreeElementCollections から要素がコピーされる 1 次元の Array 。 
		///     Array には、0 から始まるインデックス番号が必要です。</summary>
		/// 
		public void CopyTo( Array anArray )
		{
			this.CopyTo( anArray, 0 );
		}
		#endregion
		#region ' + CopyTo ( Array, Int32 ) : void 
		/// <summary>
		/// TreeElementCollection 全体を互換性のある 1 次元の Array にコピーします。
		/// コピー操作は、コピー先の配列の先頭から始まります。</summary>
		/// 
		///    <param name="anArray">
		///     TreeElementCollections から要素がコピーされる 1 次元の Array 。 
		///     Array には、0 から始まるインデックス番号が必要です。</summary>
		///    <param name="anArrayIndex">
		///    コピーの開始位置となる、anArray の 0 から始まるインデックス番号。</param>
		///    
		public void CopyTo( Array anArray, Int32 anArrayIndex )
		{
			this.CopyTo( 0, anArray, anArrayIndex, this._list.Count );
		}
		#endregion
		#region ' + CopyTo ( Int32, Array, Int32, Int32 ) : void 
		/// <summary>
		/// TreeElementCollection 全体を互換性のある 1 次元の Array にコピーします。
		/// コピー操作は、コピー先の配列の先頭から始まります。</summary>
		/// 
		///     <param name="anIndex">
		///     コピーの開始位置となる、array の 0 から始まるインデックス番号。</param>
		///     <param name="anArray">
		///     TreeElementCollection から要素がコピーされる 1 次元の Array 。 
		///     Array には、0 から始まるインデックス番号が必要です。</summary>
		///     <param name="anArrayIndex">
		///     コピーの開始位置となる、anArray の 0 から始まるインデックス位置。</summary>
		///     <param name="aCount">
		///     コピーする要素の数。</summary>
		///    
		public void CopyTo( Int32 anIndex, Array anArray, Int32 anArrayIndex, Int32 aCount )
		{
			Array.Copy( this._list.ToArray(), 0, anArray, anIndex, this._list.Count );
		}
		#endregion
		#region ' + GetEnumerator () : IEnumerator 
		/// <summary>
		/// コレクションを反復処理できる列挙子を返します。 </summary>
		/// 
		///    <returns>
		///    コレクションを反復処理するために使用できる IEnumerator。</returns>
		/// 
		public IEnumerator GetEnumerator()
		{
			return new TreeElementCollectionsEnumerator( this );
		}
		#endregion
		#region ' + GetRange ( Int32, Int32 ) : TreeElementCollection 
		/// <summary>
		/// 元の TreeElementCollections 内の要素のサブセットを表す TreeElementCollections を返します。</summary>
		///
		///    <param name="anIndex">
		///    範囲が開始する位置の、0から始まる TreeElementCollection のインデックス番号。</param>
		///    <param name="aCount">
		///    範囲内の要素の数。</param>
		///
		public TreeElementCollection GetRange( Int32 anIndex, Int32 aCount )
		{
			TreeElementCollection list = new TreeElementCollection( this.OwnerElement );
			
			list.AddRange( this._list.GetRange( anIndex, aCount ));
			
			return list;
		}
		#endregion
		#region ' + IndexOf ( TreeElement ) : Int32 
		/// <summary>
		/// 指定した TreeElement を検索し、 TreeElementCollections 全体内で最初に見つかった位置の 
		/// 0 から始まるインデックスを返します。</summary>
		///
		public Int32 IndexOf( TreeElement anItem )
		{
			return this._list.IndexOf( anItem );
		}
		#endregion
		#region ' + IndexOf ( TreeElement, Int32 ) : Int32 
		/// <summary>
		/// 指定した TreeElement を検索し、指定したインデックスから最後の要素までの
		/// TreeElementCollections のセクション内で最初に出現する位置の 
		/// 0 から始まるインデックス番号を返します。</summary>
		///
		///    <param name="anItem">
		///    TreeElementCollections 内で検索される TreeElement 。</param>
		///    <param name="aStartIndex">
		///    検索の開始位置を示す 0 から始まるインデックス。</param>
		/// 
		///    <returns>
		///    aStartIndex から最後の要素までの TreeElementCollection のセクション内で
		///    anItem が見つかった場合は、最初に見つかった位置の 0 から始まるインデックス番号。
		///    それ以外の場合は -1 。</returns>
		/// 
		public Int32 IndexOf( TreeElement anItem, Int32 aStartIndex )
		{
			return this._list.IndexOf( anItem, aStartIndex );
		}
		#endregion
		#region ' + IndexOf ( TreeElement, Int32, Int32 ) : Int32 
		/// <summary>
		/// 指定した TreeElement を検索し、指定したインデックスから始まって
		/// 指定した数の要素を格納する TreeElementCollection のセクション内で最初に出現する位置の 
		/// 0 から始まるインデックス番号を返します。</summary>
		///
		///    <param name="anItem">
		///    TreeElementCollections 内で検索される TreeElement 。</param>
		///    <param name="aStartIndex">
		///    検索の開始位置を示す 0 から始まるインデックス。</param>
		///    <param name="aCount">
		///    検索対象の範囲内にある要素の数。</param>
		/// 
		///    <returns>
		///    aStartIndex から始まって aCount 個の要素を格納する TreeElementCollection のセクション内で 
		///    value が見つかった場合は、最初に見つかった位置の 0 から始まるインデックス番号。
		///    それ以外の場合は -1 。</returns>
		/// 
		public Int32 IndexOf( TreeElement anItem, Int32 aStartIndex, Int32 aCount )
		{
			return this._list.IndexOf( anItem, aStartIndex, aCount );
		}
		#endregion
		#region ' + LastIndexOf ( TreeElement ) : Int32 
		/// <summary>
		/// TreeElementCollection 内またはその一部にある値のうち、
		/// 最後に出現する値の、0 から始まるインデックス番号を返します。</summary>
		///
		///    <param name="anItem">
		///    TreeElementCollection 内で検索される TreeElement 。</param>
		///
		public Int32 LastIndexOf( TreeElement anItem )
		{
			return this._list.LastIndexOf( anItem );
		}
		#endregion
		#region ' + LastIndexOf ( TreeElement, Int32 ) : Int32 
		/// <summary>
		/// 指定した TreeElement を検索し、最初の要素から、指定したインデックスまでの 
		/// TreeElementCollectionst のセクション内で最後に出現する位置の 
		/// 0 から始まるインデックス番号を返します。</summary>
		///
		///    <param name="anItem">
		///    TreeElementCollections 内で検索される TreeElement 。</param>
		///    <param name="aStartIndex">
		///    後方検索の開始位置を示す 0 から始まるインデックス。</param>
		///
		public Int32 LastIndexOf( TreeElement anItem, Int32 aStartIndex )
		{
			return this._list.LastIndexOf( anItem, aStartIndex );
		}
		#endregion
		#region ' + LastIndexOf ( TreeElement, Int32, Int32 ) : Int32 
		/// <summary>
		/// 指定した TreeElement を検索して、指定した数の要素を格納し、
		/// 指定したインデックスの位置で終了する TreeElementCollections のセクション内で
		/// 最初に出現する位置の 0 から始まるインデックス番号を返します。</summary>
		///
		///    <param name="anItem">
		///    TreeElementCollections 内で検索される TreeElement 。</param>
		///    <param name="aStartIndex">
		///    後方検索の開始位置を示す 0 から始まるインデックス。</param>
		///    <param name="aCount">
		///    検索対象の範囲内にある要素の数。</param>
		///
		public Int32 LastIndexOf( TreeElement anItem, Int32 aStartIndex, Int32 aCount )
		{
			return this._list.LastIndexOf( anItem, aStartIndex, aCount );
		}
		#endregion
		#region ' + Reverse () : void 
		/// <summary>
		/// TreeElementCollections 内およびその一部の要素の順序を反転させます。</summary>
		///
		public void Reverse()
		{
			this._list.Reverse();
		}
		#endregion
		#region ' + Reverse ( Int32, Int32 ) : void 
		/// <summary>
		/// 指定した範囲の要素の順序を反転させます。</summary>
		///
		///    <param name="anIndex">
		///    反転させる範囲の開始位置を示す 0 から始まるインデックス。</param>
		///    <param name="aCount">
		///    反転させる範囲内にある要素の数。</param>
		///
		public void Reverse( Int32 anIndex, Int32 aCount )
		{
			this._list.Reverse( anIndex, aCount );
		}
		#endregion
		#region ' + Sort () : void 
		/// <summary>
		/// 各要素の IComparable 実装を使用して、TreeElementCollections 全体内の要素を並べ替えます。</summary>
		///
		public void Sort()
		{
			this._list.Sort();
		}
		#endregion
		#region ' + Sort ( IComparer ) : void 
		/// <summary>
		/// 指定した比較演算子を使用して、TreeElementCollection 全体内の要素を並べ替えます。</summary>
		///
		///    <param name="aComparer">
		///    要素を比較する場合に使用する IComparer 実装。</param>
		/// 
		public void Sort( IComparer aComparer )
		{
			this._list.Sort( aComparer );
		}
		#endregion
		#region ' + Sort ( Int32, Int32, IComparer ) : void 
		/// <summary>
		/// 指定した比較演算子を使用して、TreeElementCollection のセクション内の要素を並べ替えます。</summary>
		///
		///    <param name="anIndex">
		///    並べ替える範囲の開始位置を示す 0 から始まるインデックス。</param>
		///    <param name="aCount">
		///    並べ替える範囲の長さ。</param>
		///    <param name="aComparer">
		///    要素を比較する場合に使用する IComparer 実装。</param>
		/// 
		public void Sort( Int32 anIndex, Int32 aCount, IComparer aComparer )
		{
			this._list.Sort( anIndex, aCount, aComparer );
		}
		#endregion
		#region ' + ToArray () : Object[] 
		/// <summary>
		/// TreeElementCollection の要素を
		/// 新しい配列にコピーします。</summary>
		///
		public Object[] ToArray()
		{
			if ( this._list.Count == 0 )
			{
				return new Object[] { };
			}

			Object[] newObjects = new Object[this._list.Count - 1];
			Array.Copy( this._list.ToArray(), 0, newObjects, 0, this._list.Count - 1 );

			return newObjects;
		}
		#endregion
		#region ' + TrimToSize () : void 
		/// <summary>
		/// 容量を TreeElementCollections 内にある実際の要素数に設定します。</summary>
		///
		public void TrimToSize()
		{
			this._list.TrimToSize();
		}
		#endregion
		#region '   ~ Add ( TreeElement ) : Int32 
		/// <summary>
		/// TreeElementCollection の末尾に TreeElement を追加します。</summary>
		///
		///    <param name="aValue">
		///    追加する TreeElement 。</param>
		///
		///    <returns>
		///    aValue が追加された位置のインデックス。</returns>
		/// 
		internal Int32 Add( TreeElement aValue )
		{
			return this._list.Add( aValue );
		}
		#endregion
		#region '   ~ AddRange ( ICollection ) : void 
		/// <summary>
		/// ICollection の要素を TreeElementCollections の末尾へ追加します。</summary>
		///
		///    <param name="someElements">
		///    TreeElementCollections の末尾に要素が追加される ICollection 。</param>
		///
		internal void AddRange( ICollection someElements )
		{
			foreach ( TreeElement element in someElements )
			{
				this.Add( element );
			}
		}
		#endregion
		#region '   ~ AddSubElement ( TreeElement ) : void 
		/// <summary>
		/// </summary>
		///
		internal void AddSubElement( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException( "anElement" );

			this._list.Add( anElement );
		}
		#endregion
		#region '   ~ AddRangeSubElements ( TreeElementCollection ) : void 
		/// <summary>
		/// </summary>
		///
		internal void AddRangeSubElements( TreeElementCollection someElements )
		{
			if ( someElements == null ) throw new ArgumentNullException( "someElements" );

			foreach ( TreeElement element in someElements )
			{
				this.AddSubElement( element );
			}
		}
		#endregion
		#region '   ~ Insert ( Int32, TreeElement ) : void 
		/// <summary>
		/// TreeElementCollection 内の指定したインデックスの位置に要素を挿入します。</summary>
		///
		///    <param name="anIndex">
		///    anItem を挿入する位置の、 0 から始まるインデックス番号。</param>
		///    <param name="anItem">
		///    挿入する TreeElement 。</param>
		///
		internal void Insert( Int32 anIndex, TreeElement anItem )
		{
			this._list.Insert( anIndex, anItem );
		}
		#endregion
		#region '   ~ InsertRange ( Int32, TreeElementCollection ) : void 
		/// <summary>
		/// コレクションの要素を TreeElementCollection 内の指定したインデックスの位置に挿入します。</summary>
		///
		///    <param name="anItem">
		///    新しい要素が挿入される位置の 0 から始まるインデックス。</param>
		///    <param name="someElements">
		///    TreeElementCollection に要素を挿入する ICollection 。</param>
		///
		internal void InsertRange( Int32 anItem, TreeElementCollection someElements )
		{

			foreach ( TreeElement element in someElements )
			{
				element.SetParentElement( this.OwnerElement );
			}

			this._list.InsertRange( anItem, someElements );
		}
		#endregion
		#region '   ~ Remove ( TreeElement ) : void 
		/// <summary>
		/// TreeElementCollections 内で最初に見つかった特定のオブジェクトを削除します。</summary>
		///
		///    <param name="anItem">
		///    TreeElementCollections から削除する TreeElement 。</param>
		///
		internal void Remove( TreeElement anItem )
		{
			anItem._parent = null;
			this._list.Remove( anItem );
		}
		#endregion
		#region '   ~ RemoveAt ( Int32 ) : void 
		/// <summary>
		/// TreeElementCollection の指定したインデックスにある要素を削除します。</summary>
		///
		///    <param name="anIndex">
		///    削除する要素の、0 から始まるインデックス番号。</param>
		///
		internal void RemoveAt( Int32 anIndex )
		{
			( _list[ anIndex ] as TreeElement )._parent = null;
			this._list.RemoveAt( anIndex );
		}
		#endregion
		#region '   ~ RemoveBottom () : void 
		/// <summary>
		/// コレクションの一番下の要素を削除します。</summary>
		///
		internal void RemoveBottom()
		{
			this.RemoveAt( 0 );
		}
		#endregion
		#region '   ~ RemoveTop () : void 
		/// <summary>
		/// コレクションの一番上の要素を削除します。</summary>
		///
		internal void RemoveTop()
		{
			this._list.RemoveAt( this.Count - 1 );
		}
		#endregion
		#region '   ~ RemoveRange ( Int32, Int32 ) : void 
		/// <summary>
		/// TreeElementCollections から要素の範囲を削除します。</summary>
		///
		///    <param name="anIndex">
		///    削除する要素の範囲の開始位置を示す 0 から始まるインデックス番号。</param>
		///    <param name="aCount">
		///    削除する要素の数</param>
		///
		internal void RemoveRange( Int32 anIndex, Int32 aCount )
		{
			for ( Int32 index = anIndex; index < anIndex + aCount; index++ )
			{
				this.RemoveAt( index ); 
			}
			
		}
		#endregion
		
		
		// innertype 
		#region '   - TreeElementCollectionsEnumerator : IEnumerator 
		/// <summary>
		/// コレクションに対する単純な反復処理をサポートします。</summary>
		///
		private class TreeElementCollectionsEnumerator : IEnumerator
		{
			
			// constructor 
			#region ' + constructor ( TreeElementCollection ) 
			/// <summary>
			/// 新しい TreeElementCollectionsEnumerator のインスタンスを TreeElementCollection で初期化します。</summary>
			/// 
			public TreeElementCollectionsEnumerator( TreeElementCollection aList )
			{
				this._list = aList;
			}
			#endregion
			
			
			// field 
			#region ' field 
			private TreeElementCollection _list = null;
			private Int32 _currentIndex = -1;
			#endregion
			
			
			// property 
			#region ' + Current : Object { get } 
			/// <summary>
			/// コレクション内の現在の要素を取得します。</summary>
			/// 
			public Object Current
			{
				get { return this._list[ this._currentIndex ]; }
			}
			#endregion
			
			
			// method 
			#region ' + Reset () : void 
			/// <summary>
			/// コレクションの最初の要素の前に設定します。</summary>
			/// 
			public void Reset()
			{
				this._currentIndex = -1;
			}
			#endregion
			#region ' + MoveNext () : Boolean 
			/// <summary>
			/// 列挙子をコレクションの次の要素に進めます。</summary>
			/// 
			public Boolean MoveNext()
			{
				this._currentIndex ++;

				if ( this._list.Count <=
					this._currentIndex )
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
