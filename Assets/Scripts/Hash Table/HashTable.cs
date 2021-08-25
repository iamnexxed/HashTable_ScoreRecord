

public class HashTable <Value>
{

	public class Node
	{
		public string key;
		public Value value;

		public Node()  
		{
			key = "";
			value = default(Value);
		}

		public Node(string key, Value value)
		{
			this.key = key;
			this.value = value;
		}

		public string getKey()
		{
			return key; 
		}

		public Value getValue() 
		{ 
			return value; 
		}

		public void setValue(Value obj) 
		{
			value = obj; 
		}
		public void setKey(string index) 
		{
			key = index; 
		}

	}

	//An array of Nodes
	Node[] table;

	//Number of table slots
	uint size;

	//Number of items
	uint totalItems;


	public HashTable(uint sz = 0)
	{
		size = 0;

		totalItems = 0;

		if (sz > 0)
		{
			//Resize to a prime number
			//Optimized to prevent collision
			size = getNextPrimeNum(sz);
		}

		table = new Node[size];
        for (int i = 0; i < size; i++)
        {
            table[i] = new Node();
        }
    }

	~HashTable()
	{
		purge();

	}

	public void purge()
	{
		
		table = null;

		size = 0;
		totalItems = 0;
	}

	//Determines the next prime number closest to val
	public uint getNextPrimeNum(uint val)
	{
		uint i;

		for (i = val + 1; ; i++)
		{
			if (isNumPrime(i))
				break;
		}

		return i;
	}

	//Determines whether val is a prime number
	public bool isNumPrime(uint val)
	{
		for (uint i = 2; (i * i) <= val; i++)
		{
			if ((val % i) == 0)
				return false;
		}

		return true;
	}

	//Insert value into Hash Table
	public Node insert(string key, Value value)
	{
		//Find the hash value
		uint index = hash(key);
		//Find second hash value
		uint step = doubleHash(key);

		//Return key value if hash table is full
		if(totalItems == size)
			return table[index];

		while(table[index].getValue() != null)
		{
			//If key found replace value
			if(table[index].getKey() == key)
			{
				table[index].setValue(value);

				return table[index];
			}

			//If key not found advance a slot using double hash
			index += step;
			index %= size;
		}

			//Insert slot
			table[index].setKey(key);
			table[index].setValue(value);

			++totalItems;

			return table[index];
	}

	//The hash function
	uint hash(string str)
	{
		uint hash = 0;

		for (uint i=0; i < str.Length; i++)
			hash = hash * 256 + str[(int)i];
		return hash % size;
	}


	//The second hash function
	uint doubleHash(string str)
	{
		const uint hashConst = 3;
		uint hash = 0;

		for (uint i = 0; i<str.Length; i++)
			hash = hash * 256 + str[(int)i];
		return hashConst - hash % hashConst;
	}

	//Remove key entry from Hash Table
	public void remove(string key)
	{
			//Find the hash value
			uint index = hash(key);
			uint step = doubleHash(key);
			uint originalIndex = index;

			while(table[index].getValue() != null) 
			{
				//If key found remove value
				if(table[index].getKey() == key)
				{
					table[index].key = "";
					table[index].setValue(default(Value));

					--totalItems;

					return;
				}

				//If key not found advance a slot using double hash
				index += step;
				index %= size;

				//If slots do not contain key exit
				if (originalIndex == index)
					return;
			}
	}

	//Search for a key in the Hash Table
	public Value find(string key)
	{
			//Find the hash value
			uint index = hash(key);
			uint step = doubleHash(key);
			uint originalIndex = index;

			while(table[index].getValue() != null) 
			{
				//If key is found return value
				if(table[index].getKey() == key)
				{
					return table[index].getValue();
				}

				//If key not found advance a slot using double hash
				index += step;
				index %= size;

				//If slots do not contain key return null
				if (originalIndex == index)
					return table[index].getValue();
			}

			return default(Value);
	}

	public uint getSize()
	{
		return size;
	}

	public uint getTotalItems()
	{
		return totalItems;
	}

}
