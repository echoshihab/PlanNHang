import React from "react";
import { Menu, Container, Button } from "semantic-ui-react";

interface Iprops {
  openCreateForm: () => void;
}
const NavBar: React.FC<Iprops> = ({ openCreateForm }) => {
  return (
    <Menu fixed="top" inverted pointing>
      <Container>
        <Menu.Item header>
          <img
            src="/assets/logo.png"
            alt="logo"
            style={{ marginRight: "10px" }}
          />
          PlanNHang
        </Menu.Item>
        <Menu.Item name="Activities" />
        <Menu.Item>
          <Button content="Create Activity" onClick={() => openCreateForm()} />
        </Menu.Item>
      </Container>
    </Menu>
  );
};

export default NavBar;
