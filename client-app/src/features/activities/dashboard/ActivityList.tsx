import React, { useContext, Fragment } from "react";
import { Item, Label } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import ActivityListItems from "./ActivityListItems";
import { RootStoreContext } from "../../../app/stores/rootStore";
import { format } from "date-fns";

const ActivityList = () => {
  const rootStore = useContext(RootStoreContext);
  const { activitiesByDate } = rootStore.activityStore;
  return (
    <Fragment>
      {activitiesByDate.map(([group, activities]) => (
        <Fragment key={group}>
          <Label size="large" color="violet">
            {format(group, "eeee do MMMM")}
          </Label>

          <Item.Group divided>
            {activities.map((activity) => (
              <ActivityListItems key={activity.id} activity={activity} />
            ))}
          </Item.Group>
        </Fragment>
      ))}
    </Fragment>
  );
};

export default observer(ActivityList);
