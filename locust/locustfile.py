from locust import HttpLocust, TaskSet, task

class MyTaskSet(TaskSet):
    # @task(1)
    # def values(self):
    #     self.client.get("/api/values")

    # @task(10)
    # def detailed(self):
    #     self.client.get("/api/values/detailed")

    @task
    def transient(self):
        self.client.get("/api/values/transient")

class MyLocust(HttpLocust):
    task_set = MyTaskSet
    min_wait = 100
    max_wait = 200