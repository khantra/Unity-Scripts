using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playercast : MonoBehaviour { 
	public GameObject targetobject; 
	public GameObject tmptobject;
	
	public GameObject objectofinterest;
	
	public Text interacttext; 
	public Text lumber; 
	public int wood=0;
	
	private int woodvalue=2; 
	private string targetproperty;
	
	private string targetname; 
	private int targetquantity;
	
	
	private string tmpproperty; 
	private string tmpname;
	
	private int tmpquantity; 
	private bool tmpbool;
    private backpack mybackpack;
    private GameObject player;
    private GameObject interactTXTObj;
    void Awake(){
        interacttext = GameObject.FindGameObjectWithTag("InteractText").GetComponent<Text>();
         interactTXTObj = GameObject.FindGameObjectWithTag("InteractText");
        interactTXTObj.SetActive(false);
        Debug.Log("InteractText==" + interacttext.name);
        player = GameObject.FindGameObjectWithTag("Player");
        mybackpack = player.GetComponent<backpack>();
            //GameObject.Find("HeroHDWeapons").GetComponent<backpack>();
            //GameObject.Find("Button Controller").GetComponent<backpack>();
    }

    void storeobject(storable item, string itemname, int quantity)
    {



        // 5. if player doesn't have secondary or primary weapon then equip it and add to inventory
        // if it is a weapon that is in the Primary or Secondary List check if it has mod
        //if it has mod then store it
        //if it isn't on the primary or secondary list then add the weapon to UFPS Invenotry annd update appropriate list
        //
        //1.check if item has the storable script
            backpack mback = GameObject.Find("HeroHDWeapons").GetComponent<backpack>();
        Debug.Log(">>>STOREOBJECT CHKPT 1/2 " + itemname +" database COunt: "+ mback.itemDataBaseDictionary.Count);

        if (item.GetComponent<storable>() == null)
        {
            Debug.Log("Doesn't have Storable Script ");
        }
        else {

            storable temp;
            mback.itemDataBaseDictionary.TryGetValue(itemname, out temp);
            if(temp!=null)
            Debug.Log(">>>STOREOBJECT CHKPT 3/4 temp name== " + " " + temp.getItemName()+ " "+temp.name);

            //2.check if the item Exists in the ItemDatabaseDictionary

            bool X = mback.itemDataBaseDictionary.ContainsValue(item);
            Debug.Log(">>>STOREOBJECT CHKPT ZERO X== "+ X + " "+ itemname);

            if (mback.itemDataBaseDictionary.TryGetValue(itemname, out temp))
            {
                Debug.Log(">>>STOREOBJECT CHKPT ONE " + itemname);

                // 5. if player doesn't have secondary or primary weapon then equip it and add to inventory

                Debug.Log("PLayer Has Item and Quantity is: " + temp.getItemQuantity());

                //3.if it does- Increment the quantity of 'storable' object.
                temp.setQuantity((temp.getItemQuantity() + quantity));
                storable ntemp;
                mback.itemDataBaseDictionary.TryGetValue(itemname, out ntemp);
                Debug.Log(">>>STOREOBJECT CHKPT TWO "+ itemname);

                Debug.Log(ntemp.name + " Has its Quantity Increaseed to: " + ntemp.getItemQuantity());
                targetobject.SetActive(false);

            }
            else
            {
                // 4. if it doesn-t Add to inventory
                Debug.Log("Player Doesn't Have Item in Itemdatabase with a count of: " + mback.itemDataBaseDictionary.Count);

                mback.itemDataBaseDictionary.Add(itemname, item);
                      Debug.Log(">>>STOREOBJECT CHKPT THREE " + mback.itemDataBaseDictionary.Count);
          storable ntemp;
                mback.itemDataBaseDictionary.TryGetValue(itemname, out ntemp);
                if (ntemp == null)
                    Debug.Log("Nothing was added!!!");

                Debug.Log("THe Following Item Now Exists: " + ntemp.getItemName());
                Debug.Log("Itemdatabase Now Has a Count OF: " + mback.itemDataBaseDictionary.Count);

            }


            //weapons

            //non Weapons
            //      Debug.Log("Itemdatabase count== " + mback.itemDataBaseDictionary.Count);
            //  mback.itemDataBaseDictionary.Add(itemname, item);
            //   Debug.Log("Itemdatabase count== " + mback.itemDataBaseDictionary.Count);

            //    Debug.Log("Search Itemdatabse  for itemname== " + temp.name);

            targetobject.SetActive(false);
    }

    }
    void inspectobject(GameObject x) {
		
				
		if(x.tag=="Storableobject"){

            interactTXTObj.SetActive(true);
            objectofinterest =GameObject.FindWithTag("InteractText");
								
			interacttext = objectofinterest.GetComponent<Text> ();
									
			interacttext.text = "Press E to Interact";


        }


        else if(x==GameObject.FindWithTag("engine")){
            interactTXTObj.SetActive(true);
            objectofinterest =GameObject.FindWithTag("InteractText");
								
			interacttext = objectofinterest.GetComponent<Text> ();
						
			interacttext.text = "Press E to Add Lumber";  
			
		}
        else
        {
            interactTXTObj.SetActive(true);
            interacttext.text = "nothing";
            interactTXTObj.SetActive(false);
        }
		
	}
	
	
	void ignoreobject(){
        interactTXTObj.SetActive(true);
        interacttext.text=" ";
        interactTXTObj.SetActive(false);
    }
	

	
	void giveobject(GameObject y){
			
		if(y==GameObject.Find("engine")){
					
			objectofinterest=GameObject.Find("Temperature");
					
			thermometer tempup = objectofinterest.GetComponent<thermometer> ();
			
			tempup.CurrentTemp+=(wood*woodvalue);
			
			tmptobject=GameObject.Find("ingredients");
								
			lumber=tmptobject.GetComponent<Text>();
			
			wood -= wood; 
						
			lumber.text="Lumber: " +wood;

        }
		
	}
	
	void Update() { 
		
		RaycastHit hit;
		
		Vector3 forward = transform.TransformDirection (Vector3.forward) * 10; 
		
		Debug.DrawRay (transform.position, forward, Color.red);

		
		if(Physics.Raycast(transform.position, (forward) , out hit, 5)){
            GameObject x = hit.collider.gameObject;
            print("Hit Object: " + x.name);


            if (hit.collider.gameObject.tag =="engine"){
									
				targetname= hit.collider.gameObject.transform.tag;
												
				targetobject=hit.collider.gameObject;
										
				print("This Object is usable and it's tag is:" + targetname);
												
				inspectobject(targetobject);
										
				if(Input.GetKeyDown(KeyCode.E)){
			
					giveobject(targetobject);
					wood=-wood;			
				}
			
			}
			
          //  targetobject = hit.collider.gameObject;
            if (hit.collider.gameObject.tag == "Storableobject")
            {
                Debug.Log("PlayerCast- This is a Storableobject ");

                targetname = hit.collider.gameObject.transform.tag;

                targetobject = hit.collider.gameObject;
				
				usableobject colliding = targetobject.GetComponent<usableobject>();

                targetproperty =colliding.objectproperty;
				
				targetquantity=colliding.objectquantity;
				
				inspectobject(targetobject); 
				
				print("This Object is usable and it's tag is:" + targetname);
					
				if(Input.GetKeyDown(KeyCode.E)){
                    if (targetobject.GetComponent<storable>() != false)
                    {
                        storable newstorable = targetobject.GetComponent<storable>();
                        //    storeobject(targetobject, targetname, targetquantity);
                        print(">>>>newstorable.itemname:" + newstorable.itemname);

                        storeobject(newstorable, newstorable.itemname, targetquantity);
                    }
			
				}
				
			}
			
				
			else{
									
				print("This Object is notusable"); 
							
			}
			
					
			print("There object Tag is called:" +targetname );
						
		}
		
		
	else{
            ignoreobject();
        }
		
		
		
	}
}