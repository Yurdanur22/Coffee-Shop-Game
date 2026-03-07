using UnityEngine;
using UnityEngine.AI;
public class CustomerAI : MonoBehaviour
{
    /*
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject[] tables = GameObject.FindGameObjectsWithTag("Chair");

        if (tables.Length == 0) return;

        GameObject target = tables[Random.Range(0, tables.Length)];
        agent.SetDestination(target.transform.position);
    }
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped = true;
        }
    }*/

    NavMeshAgent agent;

    GameObject targetTable;
    bool hasTable = false;
    bool isSitting = false;

    float sitTimer = 0f;
    public float sitDuration = 7f; // 👈 oturma süresi

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true; // kapıda bekle
    }
    void Update()
    {
        // 🪑 Boş masa ara
        if (!hasTable)
        {
            GameObject[] tables = GameObject.FindGameObjectsWithTag("Chair");

            foreach (GameObject table in tables)
            {
                if (table.transform.childCount == 0)
                {
                    targetTable = table;
                    hasTable = true;

                    // masayı rezerve et
                    transform.SetParent(table.transform);

                    agent.isStopped = false;
                    agent.SetDestination(table.transform.position);
                    break;
                }
            }
        }
        // 🪑 Masaya ulaştıysa
        if (hasTable && !isSitting &&
            !agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped = true;
            isSitting = true;
            sitTimer = 0f;
        }
        // ⏱️ Oturma süresi say
        if (isSitting)
        {
            sitTimer += Time.deltaTime;

            if (sitTimer >= sitDuration)
            {
                LeaveTable();
            }
        }
    }
    void LeaveTable()
    {
        // masayı boşalt
        transform.SetParent(null);

        // müşteriyi sahneden sil
        Destroy(gameObject);
    }

}
