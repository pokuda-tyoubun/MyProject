using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;


namespace FxCommonLib.Utils {
    /// <summary>
    /// キーと値のペアのインデックス付きの厳密に型指定されたオブジェクトのリストを表します。
    /// HACK MSTestを実装
    /// </summary>
    /// <typeparam name="TKey">リスト内のキーの型</typeparam>
    /// <typeparam name="TValue">リスト内の値の型</typeparam>
    public interface IOrderedDictionary<TKey, TValue> :
        IOrderedDictionary,
        IDictionary<TKey, TValue> {
        /// <summary>
        /// 指定したキーおよび値を持つエントリを、使用できる最小のインデックスを持つ OrderedDictionary コレクションに追加します。
        /// </summary>
        /// <param name="key">追加するエントリのキー。</param>
        /// <param name="value">追加するエントリの値。この値は null 参照 (Visual Basic では Nothing) に設定できます。</param>
        new void Add(TKey key, TValue value);

        /// <summary>
        /// OrderedDictionary コレクションの指定したインデックス位置に、指定したキーと値を持つ新しいエントリを挿入します。  
        /// </summary>
        /// <param name="index">要素 を挿入する位置の、0 から始まるインデックス番号。</param>
        /// <param name="key">追加するエントリのキー。</param>
        /// <param name="value">追加するエントリの値。値は null 参照 (Visual Basic では Nothing) に設定できます。</param>
        void Insert(int index, TKey key, TValue value);

        /// <summary>
        /// 指定したインデックス位置にある値を取得または設定します。
        /// </summary>
        /// <param name="index">取得または設定する値の、0 から始まるインデックス番号。</param>
        /// <returns></returns>
        new TValue this[int index] { get; set; }
    }

    /// <summary>
    /// ジェネリックなOrderedDictionary
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class OrderedDictionary<TKey, TValue> :
    IOrderedDictionary<TKey, TValue>,
    ISerializable,
    IDeserializationCallback {
        private const int DefaultCapacity = 0;

        #region static readonly
        private static readonly string _keyTypeName = typeof(TKey).FullName;
        private static readonly string _valueTypeName = typeof(TValue).FullName;
        private static readonly bool _valueTypeIsReferenceType = !typeof(ValueType).IsAssignableFrom(typeof(TValue));
        #endregion

        private Dictionary<TKey, TValue> _dictionary;
        private List<KeyValuePair<TKey, TValue>> _list;
        private IEqualityComparer<TKey> _comparer;
        private object _syncRoot;
        private int _capacity;

        #region Constructor

        /// <summary>
        /// OrderedDictionary クラスの新しいインスタンスを初期化します。
        /// </summary>
        public OrderedDictionary() : this(DefaultCapacity, null) { }

        /// <summary>
        /// 指定した初期容量を使用して、OrderedDictionary クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="capacity">初期容量</param>
        public OrderedDictionary(int capacity)
            : this(capacity, null)
        { }

        /// <summary>
        /// 比較演算子を指定して、OrderedDictionary クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="comparer">比較演算子</param>
        public OrderedDictionary(IEqualityComparer<TKey> comparer)
            : this(DefaultCapacity, comparer)
        { }

        /// <summary>
        /// 指定した初期容量および比較演算子を使用して、OrderedDictionary クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="capacity">初期容量</param>
        /// <param name="comparer">比較演算子</param>
        public OrderedDictionary(int capacity, IEqualityComparer<TKey> comparer) {
            if (0 > capacity)
                throw new ArgumentOutOfRangeException("capacity", "負の値は指定できません。");

            _capacity = capacity;
            _comparer = comparer;
        }

        /// <summary>
        /// デシリアライズのためのコンストラクタ
        /// </summary>
        /// <param name="info">SerializationInfo</param>
        /// <param name="context">StreamingContext</param>
        public OrderedDictionary(SerializationInfo info, StreamingContext context) {
            _dictionary = (Dictionary<TKey, TValue>)info.GetValue("dictionary", typeof(Dictionary<TKey, TValue>));
            _list = (List<KeyValuePair<TKey, TValue>>)info.GetValue("list", typeof(List<KeyValuePair<TKey, TValue>>));
            _comparer = (IEqualityComparer<TKey>)info.GetValue("comparer", typeof(IEqualityComparer<TKey>));
            _syncRoot = info.GetValue("syncRoot", typeof(object));
            _capacity = info.GetInt32("capacity");
        }

        #endregion

        /// <summary>
        /// Dictionaryを取得します。
        /// </summary>
        private Dictionary<TKey, TValue> Dictionary {
            get {
                if (null == _dictionary) {
                    _dictionary = new Dictionary<TKey, TValue>(_capacity, _comparer);
                }
                return _dictionary;
            }
        }

        /// <summary>
        /// KeyValuePairのリストを取得します。
        /// </summary>
        private List<KeyValuePair<TKey, TValue>> List {
            get {
                if (null == _list) {
                    _list = new List<KeyValuePair<TKey, TValue>>(_capacity);
                }
                return _list;
            }
        }

        /// <summary>
        /// OrderedDictionary コレクションの指定したインデックス位置に、指定したキーと値を持つ新しいエントリを挿入します。  
        /// </summary>
        /// <param name="index">要素 を挿入する位置の、0 から始まるインデックス番号。</param>
        /// <param name="key">追加するエントリのキー。</param>
        /// <param name="value">追加するエントリの値。値は null 参照 (Visual Basic では Nothing) に設定できます。</param>
        public void Insert(int index, TKey key, TValue value) {
            if (index > Count || index < 0)
                throw new ArgumentOutOfRangeException("index");

            Dictionary.Add(key, value);
            List.Insert(index, new KeyValuePair<TKey, TValue>(key, value));
        }

        /// <summary>
        /// 指定したインデックス位置にあるエントリを OrderedDictionary コレクションから削除します。
        /// </summary>
        /// <param name="index">削除する要素の 0 から始まるインデックス。</param>
        public void RemoveAt(int index) {
            if (index >= Count || index < 0)
                throw new ArgumentOutOfRangeException("index");

            TKey key = List[index].Key;

            List.RemoveAt(index);
            Dictionary.Remove(key);
        }

        /// <summary>
        /// 指定したインデックス位置にある値を取得または設定します。
        /// </summary>
        /// <param name="index">取得または設定する値の、0 から始まるインデックス番号。</param>
        /// <returns></returns>
        public TValue this[int index] {
            get { return List[index].Value; }
            set {
                if (index >= Count || index < 0)
                    throw new ArgumentOutOfRangeException("index");

                TKey key = List[index].Key;

                List[index] = new KeyValuePair<TKey, TValue>(key, value);
                Dictionary[key] = value;
            }
        }

        /// <summary>
        /// 指定したキーおよび値を持つエントリを、使用できる最小のインデックスを持つ OrderedDictionary コレクションに追加します。
        /// </summary>
        /// <param name="key">追加するエントリのキー。</param>
        /// <param name="value">追加するエントリの値。この値は null 参照 (Visual Basic では Nothing) に設定できます。</param>
        public void Add(TKey key, TValue value) {
            Dictionary.Add(key, value);
            List.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        /// <summary>
        /// OrderedDictionary コレクションからすべての要素を削除します。 
        /// </summary>
        public void Clear() {
            Dictionary.Clear();
            List.Clear();
        }

        /// <summary>
        /// OrderedDictionary コレクションに特定のキーが格納されているかどうかを判断します。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key) {
            return Dictionary.ContainsKey(key);
        }

        /// <summary>
        /// OrderedDictionary コレクションに特定の値が格納されているかどうかを判断します。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ContainsValu(TValue value) {
            return Dictionary.ContainsValue(value);
        }

        /// <summary>
        /// OrderedDictionary コレクションが読み取り専用かどうかを示す値を取得します。 
        /// </summary>
        public bool IsReadOnly {
            get { return false; }
        }

        /// <summary>
        /// 指定したキー項目のコレクション内での位置インデックスを取得します。
        /// </summary>
        /// <param name="key">キー項目</param>
        /// <returns></returns>
        public int IndexOfKey(TKey key) {
            if (null == key)
                throw new ArgumentNullException("key");

            foreach (var item in List.Select((x, i) => new { Value = x, Index = i })) {
                var entry = List[item.Index];
                TKey next = entry.Key;
                if (null != _comparer) {
                    if (_comparer.Equals(next, key)) {
                        return item.Index;
                    }
                } else if (next.Equals(key)) {
                    return item.Index;
                }
            }
            return -1;
        }

        /// <summary>
        /// 指定したキーを持つエントリを OrderedDictionary コレクションから削除します。
        /// </summary>
        /// <param name="key">削除するエントリのキー。</param>
        /// <returns></returns>
        public bool Remove(TKey key) {
            if (null == key)
                throw new ArgumentNullException("key");

            int index = IndexOfKey(key);
            if (index >= 0) {
                if (Dictionary.Remove(key)) {
                    List.RemoveAt(index);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 指定したキーの値を取得または設定します。 
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns></returns>
        public TValue this[TKey key] {
            get { return Dictionary[key]; }
            set {
                if (Dictionary.ContainsKey(key)) {
                    Dictionary[key] = value;
                    List[IndexOfKey(key)] = new KeyValuePair<TKey, TValue>(key, value);
                    return;
                }
                Add(key, value);
            }
        }

        /// <summary>
        /// OrderedDictionary コレクションに格納されているキー/値ペアの数を取得します。 
        /// </summary>
        public int Count {
            get { return List.Count; }
        }

        /// <summary>
        /// OrderedDictionary コレクションのキーを保持している ICollection オブジェクトを取得します。
        /// </summary>
        public ICollection<TKey> Keys {
            get { return Dictionary.Keys; }
        }

        /// <summary>
        /// 指定したキーに関連付けられている値を取得します。
        /// </summary>
        /// <param name="key">値を取得する対象のキー。</param>
        /// <param name="value">このメソッドが返されるときに、キーが見つかった場合は、指定したキーに関連付けられている値。それ以外の場合は value パラメータの型に対する既定の値。このパラメータは初期化せずに渡されます。</param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value) {
            return Dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// OrderedDictionary コレクションの値を保持している ICollection オブジェクトを取得します。 
        /// </summary>
        public ICollection<TValue> Values {
            get { return Dictionary.Values; }
        }

        /// <summary>
        /// TKey型へ変換します。
        /// </summary>
        /// <param name="keyObject"></param>
        /// <returns></returns>
        private static TKey ConvertToKeyType(object key) {
            if (null == key)
                throw new ArgumentNullException("key");

            if (key is TKey)
                return (TKey)key;

            throw new ArgumentException("型変換できません。" + _keyTypeName, "key");
        }

        /// <summary>
        /// TValue型へ変換します。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static TValue ConvertToValueType(object value) {
            if (null == value) {
                if (_valueTypeIsReferenceType)
                    return default(TValue);
                throw new ArgumentNullException("value");
            }
            if (value is TValue) return (TValue)value;

            throw new ArgumentException("型変換できません。 " + _valueTypeName, "value");
        }

        #region IOrderedDictionary<TKey,TValue> メンバ

        void IOrderedDictionary<TKey, TValue>.Add(TKey key, TValue value) {
            Add(key, value);
        }

        void IOrderedDictionary<TKey, TValue>.Insert(int index, TKey key, TValue value) {
            Insert(index, key, value);
        }

        TValue IOrderedDictionary<TKey, TValue>.this[int index] {
            get { return this[index]; }
            set { this[index] = value; }
        }

        #endregion

        #region IOrderedDictionary メンバ

        IDictionaryEnumerator IOrderedDictionary.GetEnumerator() {
            return Dictionary.GetEnumerator();
        }

        void IOrderedDictionary.Insert(int index, object key, object value) {
            Insert(index, ConvertToKeyType(key), ConvertToValueType(value));
        }

        void IOrderedDictionary.RemoveAt(int index) {
            RemoveAt(index);
        }

        object IOrderedDictionary.this[int index] {
            get { return this[index]; }
            set { this[index] = ConvertToValueType(value); }
        }

        #endregion

        #region IDictionary メンバ

        void IDictionary.Add(object key, object value) {
            Add(ConvertToKeyType(key), ConvertToValueType(value));
        }

        void IDictionary.Clear() {
            this.Clear();
        }

        bool IDictionary.Contains(object key) {
            return ContainsKey(ConvertToKeyType(key));
        }

        IDictionaryEnumerator IDictionary.GetEnumerator() {
            return Dictionary.GetEnumerator();
        }

        bool IDictionary.IsFixedSize {
            get { return false; }
        }

        bool IDictionary.IsReadOnly {
            get { return false; }
        }

        ICollection IDictionary.Keys {
            get { return (ICollection)Keys; }
        }

        void IDictionary.Remove(object key) {
            Remove(ConvertToKeyType(key));
        }

        ICollection IDictionary.Values {
            get { return (ICollection)Values; }
        }

        object IDictionary.this[object key] {
            get { return this[ConvertToKeyType(key)]; }
            set { this[ConvertToKeyType(key)] = ConvertToValueType(value); }
        }

        #endregion

        #region ICollection メンバ

        void ICollection.CopyTo(Array array, int index) {
            ((ICollection)List).CopyTo(array, index);
        }

        int ICollection.Count {
            get { return List.Count; }
        }

        bool ICollection.IsSynchronized {
            get { return false; }
        }

        object ICollection.SyncRoot {
            get {
                if (this._syncRoot == null) {
                    System.Threading.Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
                }
                return this._syncRoot;
            }
        }

        #endregion

        #region IEnumerable メンバ

        IEnumerator IEnumerable.GetEnumerator() {
            return List.GetEnumerator();
        }

        #endregion

        #region IDictionary<TKey,TValue> メンバ

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value) {
            Add(key, value);
        }

        bool IDictionary<TKey, TValue>.ContainsKey(TKey key) {
            return this.ContainsKey(key);
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys {
            get { return Dictionary.Keys; }
        }

        bool IDictionary<TKey, TValue>.Remove(TKey key) {
            return Remove(key);
        }

        bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value) {
            return this.TryGetValue(key, out value);
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values {
            get { return Dictionary.Values; }
        }

        TValue IDictionary<TKey, TValue>.this[TKey key] {
            get { return this[key]; }
            set { this[key] = value; }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>> メンバ

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) {
            Add(item.Key, item.Value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear() {
            this.Clear();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) {
            return ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
            ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).CopyTo(array, arrayIndex);
        }

        int ICollection<KeyValuePair<TKey, TValue>>.Count {
            get { return List.Count; }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly {
            get { return false; }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) {
            return Remove(item.Key);
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> メンバ

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() {
            return List.GetEnumerator();
        }

        #endregion

        #region ISerializable メンバ

        /// <summary>
        /// シリアル化します。
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("dictionary", _dictionary);
            info.AddValue("list", _list);
            info.AddValue("comparer", _comparer);
            info.AddValue("syncRoot", _syncRoot);
            info.AddValue("capacity", _capacity);
        }

        #endregion

        #region IDeserializationCallback メンバ

        /// <summary>
        /// オブジェクト グラフ全体が逆シリアル化された時点で実行します。
        /// </summary>
        /// <param name="sender"></param>
        void IDeserializationCallback.OnDeserialization(object sender) {
            //特にやることないと思う、たぶん
        }
        #endregion
    }
}