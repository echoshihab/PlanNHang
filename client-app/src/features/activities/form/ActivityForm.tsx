import React from "react";
import { Segment, Form, Button } from "semantic-ui-react";

interface Iprops {
  setEditMode: (editMode: boolean) => void;
}

const ActivityForm: React.FC<Iprops> = ({ setEditMode }) => {
  return (
    <Segment clearing>
      <Form>
        <Form.Input placeholder="Title" />
        <Form.Input placeholder="Description" />
        <Form.Input placeholder="Category" />
        <Form.Input type="date" placeholder="Date" />
        <Form.Input placeholder="City" />
        <Form.Input placeholder="Venue" />
        <Button floated="right" positive type="submit" content="Submit" />
        <Button
          onClick={() => setEditMode(false)}
          floated="right"
          type="button"
          content="Cancel"
        />
      </Form>
    </Segment>
  );
};

export default ActivityForm;
